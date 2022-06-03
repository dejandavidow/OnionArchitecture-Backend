using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;using Contracts.Exceptions;
internal sealed class TimeSheetRepository : ITimeSheetRepository
{
    private readonly RepositoryDbContext _dbContext;
    public TimeSheetRepository(RepositoryDbContext dbContext)
    {
        _dbContext=dbContext;
    }
    public async Task<IEnumerable<TimeSheet>> GetTimeSheetAsync(CancellationToken cancellationToken = default)
    {
        return (await _dbContext.TimeSheets
        .Include(x => x.Client)
        .Include(x => x.Project)
        .ThenInclude(x => x.Member)
        .Include(x => x.Category)
        .AsNoTracking()
        .ToListAsync(cancellationToken))
        .Select(timesheet => new TimeSheet(
            timesheet.Id,
            timesheet.Description,
            timesheet.Time,
            timesheet.OverTime,
            timesheet.Date,
            new Client(timesheet.Client.Id,timesheet.Client.ClientName,timesheet.Client.Adress,timesheet.Client.City,timesheet.Client.PostalCode,timesheet.Client.Country),
            new Project(timesheet.Project.Id,timesheet.Project.ProjectName,timesheet.Project.Description,timesheet.Project.Archive,timesheet.Project.Status,
            new Client(timesheet.Client.Id,timesheet.Client.ClientName,timesheet.Client.Adress,timesheet.Client.City,timesheet.Client.PostalCode,timesheet.Client.Country),
            new Member(timesheet.Project.Member.Id,timesheet.Project.Member.Name,timesheet.Project.Member.Username,timesheet.Project.Member.Email,timesheet.Project.Member.Hours,timesheet.Project.Member.Status,timesheet.Project.Member.Role,timesheet.Project.Member.Password)),
            new Category(timesheet.Category.Id,timesheet.Category.Name)
        ));
    }

    public async Task<TimeSheet> GetTimeSheetById(Guid id, CancellationToken cancellationToken = default)
    {
        var timesheet = await _dbContext.TimeSheets
        .Include(x => x.Category)
        .Include(x => x.Client)
        .Include(x => x.Project)
        .ThenInclude( x => x.Member).AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        return new TimeSheet
        (
            timesheet.Id,
            timesheet.Description,
            timesheet.Time,
            timesheet.OverTime,
            timesheet.Date,
            new Client(timesheet.Client.Id,timesheet.Client.ClientName,timesheet.Client.Adress,timesheet.Client.City,timesheet.Client.PostalCode,timesheet.Client.Country),
            new Project(timesheet.Project.Id,timesheet.Project.ProjectName,timesheet.Project.Description,timesheet.Project.Archive,timesheet.Project.Status,
            new Client(timesheet.Client.Id,timesheet.Client.ClientName,timesheet.Client.Adress,timesheet.Client.City,timesheet.Client.PostalCode,timesheet.Client.Country),
            new Member(timesheet.Project.Member.Id,timesheet.Project.Member.Name,timesheet.Project.Member.Username,timesheet.Project.Member.Email,timesheet.Project.Member.Hours,timesheet.Project.Member.Status,timesheet.Project.Member.Role,timesheet.Project.Member.Password)),
            new Category(timesheet.Category.Id,timesheet.Category.Name)
        );
    }

    public async Task InsertTimeSheet(TimeSheet timeSheet,CancellationToken cancellationToken)
    {
        var checkTS = await _dbContext.TimeSheets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == timeSheet.Id,cancellationToken);
        if(checkTS != null)
        {
            throw new EntityAlreadyExists("Timesheet already exists.");
        }
       await _dbContext.TimeSheets.AddAsync(new Persistence.Models.PersistenceTimeSheet()
       {
           Id = timeSheet.Id,
           Description = timeSheet.Description,
           Time =timeSheet.Time,
           OverTime = timeSheet.OverTime,
           Date = timeSheet.Date,
           ClientId = timeSheet.Client.Id,
           ProjectId = timeSheet.Project.Id,
           CategoryId = timeSheet.Category.Id
       });
    }

    public void RemoveTimeSheet(TimeSheet timeSheet)
    {
        _dbContext.TimeSheets.Remove( new Persistence.Models.PersistenceTimeSheet()
        {
            Id = timeSheet.Id,
           Description = timeSheet.Description,
           Time =timeSheet.Time,
           OverTime = timeSheet.OverTime,
           Date = timeSheet.Date,
           Client = new Persistence.Models.PersistenceClient
           {
                Id = timeSheet.Client.Id,
               ClientName = timeSheet.Client.ClientName,
               Adress = timeSheet.Client.Adress,
               City = timeSheet.Client.City,
               PostalCode = timeSheet.Client.PostalCode,
               Country = timeSheet.Client.Country
           },
          Project = new Persistence.Models.PersistenceProject
          {
               Id = timeSheet.Project.Id,
                   ProjectName = timeSheet.Project.Description,
                   Archive = timeSheet.Project.Archive,
                   Status = timeSheet.Project.Status,
                   Client = new Persistence.Models.PersistenceClient()
                   {
                    Id = timeSheet.Client.Id,
                     ClientName = timeSheet.Project.Client.ClientName,
                    Adress = timeSheet.Project.Client.Adress,
                    City = timeSheet.Project.Client.City,
                    PostalCode = timeSheet.Project.Client.PostalCode,
                    Country = timeSheet.Project.Client.Country
                   },
                   Member = new Persistence.Models.PersistenceMember()
                   {
                        Id  = timeSheet.Project.Member.Id,
                        Name = timeSheet.Project.Member.Name,
                        Username = timeSheet.Project.Member.Username,
                        Email = timeSheet.Project.Member.Email,
                        Hours = timeSheet.Project.Member.Hours,
                        Status = timeSheet.Project.Member.Status,
                        Role = timeSheet.Project.Member.Role
                   }
          },
         Category = new Persistence.Models.PersistenceCategory()
          {
              Id = timeSheet.Category.Id,
              Name = timeSheet.Category.Name
          }           
        });
    }
    public async Task UpdateTimeSheet(TimeSheet timesheet,CancellationToken cancellationToken = default)
    {
        var persTS = await _dbContext.TimeSheets.FirstOrDefaultAsync(x => x.Id == timesheet.Id);
        persTS.Id = timesheet.Id;
        persTS.Description = timesheet.Description;
        persTS.Time = timesheet.Time;
        persTS.OverTime = timesheet.OverTime;
        persTS.ClientId = timesheet.Client.Id;
        persTS.ProjectId = timesheet.Project.Id;
        persTS.CategoryId = timesheet.Category.Id;
    }
}
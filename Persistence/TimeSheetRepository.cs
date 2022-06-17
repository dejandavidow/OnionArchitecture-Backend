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
        _dbContext = dbContext;
    }
    public async Task<int> GetCount(TimeSheetParams timesheetParams)
    {
        if(timesheetParams.FilterStart == null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId == null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return await _dbContext.TimeSheets.CountAsync();
        }
        else if (timesheetParams.FilterStart != null && timesheetParams.FilterEnd != null && timesheetParams.CategoryId == null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return await _dbContext.TimeSheets.Where(x => (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd)).CountAsync();
        }
        else if (timesheetParams.FilterStart == null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId != null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return await _dbContext.TimeSheets.Where(x => x.Category.Id.ToString() == timesheetParams.CategoryId).CountAsync();
        }
        else if (timesheetParams.FilterStart == null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId == null && timesheetParams.ClientId != null && timesheetParams.ProjectId == null)
        {
            return await _dbContext.TimeSheets.Where(x => x.Client.Id.ToString() == timesheetParams.ClientId).CountAsync();
        }
        else if (timesheetParams.FilterStart == null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId == null && timesheetParams.ClientId == null && timesheetParams.ProjectId != null)
        {
            return await _dbContext.TimeSheets.Where(x => x.Project.Id.ToString() == timesheetParams.ProjectId).CountAsync();
        }
        else if (timesheetParams.FilterStart != null && timesheetParams.FilterEnd != null && timesheetParams.CategoryId != null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return await _dbContext.TimeSheets.Where(x => x.Category.Id.ToString() == timesheetParams.CategoryId && (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd)).CountAsync();
        }
        else if (timesheetParams.FilterStart != null && timesheetParams.FilterEnd != null && timesheetParams.CategoryId != null && timesheetParams.ClientId != null && timesheetParams.ProjectId == null)
        {
            return await _dbContext.TimeSheets.Where(x => x.Category.Id.ToString() == timesheetParams.CategoryId && x.Client.Id.ToString() == timesheetParams.ClientId && (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd)).CountAsync();
        }
        else
        {
            return await _dbContext.TimeSheets.Where(x => x.Project.Id.ToString() == timesheetParams.ProjectId && x.Category.Id.ToString() == timesheetParams.CategoryId && x.Client.Id.ToString() == timesheetParams.ClientId && (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd)).CountAsync();
        }
    }
    public async Task<IEnumerable<TimeSheet>> GetFilteredTS(TimeSheetParams timesheetParams, CancellationToken cancellationToken = default)
    {
        if (timesheetParams.FilterStart == null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId == null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return (await _dbContext.TimeSheets
           .OrderBy(x => x.Date)
           .Include(x => x.Client)
           .Include(x => x.Project)
           .ThenInclude(x => x.Member)
           .Include(x => x.Category)
           .Skip((timesheetParams.PageNumber - 1) * timesheetParams.PageSize)
           .Take(timesheetParams.PageSize)
           .ToListAsync(cancellationToken))
           .Select(timesheet => new TimeSheet(
               timesheet.Id,
               timesheet.Description,
               timesheet.Time,
               timesheet.OverTime,
               timesheet.Date,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
               new Category(timesheet.Category.Id, timesheet.Category.Name)
           ));
        }
        else if (timesheetParams.FilterStart != null && timesheetParams.FilterEnd != null && timesheetParams.CategoryId == null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return (await _dbContext.TimeSheets
           .Where(x => (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd))
           .Include(x => x.Client)
           .Include(x => x.Project)
           .ThenInclude(x => x.Member)
           .Include(x => x.Category)
           .ToListAsync(cancellationToken))
           .Select(timesheet => new TimeSheet(
               timesheet.Id,
               timesheet.Description,
               timesheet.Time,
               timesheet.OverTime,
               timesheet.Date,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
               new Category(timesheet.Category.Id, timesheet.Category.Name)
           ));
        }
        else if (timesheetParams.FilterStart == null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId != null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return (await _dbContext.TimeSheets
           .Where(x => x.Category.Id.ToString() == timesheetParams.CategoryId)
           .Include(x => x.Client)
           .Include(x => x.Project)
           .ThenInclude(x => x.Member)
           .Include(x => x.Category)
           .ToListAsync(cancellationToken))
           .Select(timesheet => new TimeSheet(
               timesheet.Id,
               timesheet.Description,
               timesheet.Time,
               timesheet.OverTime,
               timesheet.Date,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
               new Category(timesheet.Category.Id, timesheet.Category.Name)
           ));
        }
        else if (timesheetParams.FilterStart == null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId == null && timesheetParams.ClientId != null && timesheetParams.ProjectId == null)
        {
            return (await _dbContext.TimeSheets
           .Where(x => x.Client.Id.ToString() == timesheetParams.ClientId)
           .Include(x => x.Client)
           .Include(x => x.Project)
           .ThenInclude(x => x.Member)
           .Include(x => x.Category)
           .ToListAsync(cancellationToken))
           .Select(timesheet => new TimeSheet(
               timesheet.Id,
               timesheet.Description,
               timesheet.Time,
               timesheet.OverTime,
               timesheet.Date,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
               new Category(timesheet.Category.Id, timesheet.Category.Name)
           ));
        }
        else if (timesheetParams.FilterStart== null && timesheetParams.FilterEnd == null && timesheetParams.CategoryId == null && timesheetParams.ClientId == null && timesheetParams.ProjectId != null)
        {
            return (await _dbContext.TimeSheets
           .Where(x => x.Project.Id.ToString() == timesheetParams.ProjectId)
           .Include(x => x.Client)
           .Include(x => x.Project)
           .ThenInclude(x => x.Member)
           .Include(x => x.Category)
           .ToListAsync(cancellationToken))
           .Select(timesheet => new TimeSheet(
               timesheet.Id,
               timesheet.Description,
               timesheet.Time,
               timesheet.OverTime,
               timesheet.Date,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
               new Category(timesheet.Category.Id, timesheet.Category.Name)
           ));
        }
        else if (timesheetParams.FilterStart != null && timesheetParams.FilterEnd != null && timesheetParams.CategoryId != null && timesheetParams.ClientId == null && timesheetParams.ProjectId == null)
        {
            return (await _dbContext.TimeSheets
          .Where(x => x.Category.Id.ToString() == timesheetParams.CategoryId && (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd))
          .Include(x => x.Client)
          .Include(x => x.Project)
          .ThenInclude(x => x.Member)
          .Include(x => x.Category)
          .ToListAsync(cancellationToken))
          .Select(timesheet => new TimeSheet(
              timesheet.Id,
              timesheet.Description,
              timesheet.Time,
              timesheet.OverTime,
              timesheet.Date,
              new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
              new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
              new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
              new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
              new Category(timesheet.Category.Id, timesheet.Category.Name)
          ));
        }
        else if (timesheetParams.FilterStart != null && timesheetParams.FilterEnd != null && timesheetParams.CategoryId != null && timesheetParams.ClientId != null && timesheetParams.ProjectId == null)
        {
            return (await _dbContext.TimeSheets
            .Where(x => x.Category.Id.ToString() == timesheetParams.CategoryId && x.Client.Id.ToString() == timesheetParams.ClientId && (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd))
            .Include(x => x.Client)
            .Include(x => x.Project)
            .ThenInclude(x => x.Member)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken))
            .Select(timesheet => new TimeSheet(
                timesheet.Id,
                timesheet.Description,
                timesheet.Time,
                timesheet.OverTime,
                timesheet.Date,
                new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
                new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
                new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
                new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
                new Category(timesheet.Category.Id, timesheet.Category.Name)
            ));
        }
        else
        {
            return (await _dbContext.TimeSheets
           .Where(x => x.Project.Id.ToString() == timesheetParams.ProjectId && x.Category.Id.ToString() == timesheetParams.CategoryId && x.Client.Id.ToString() == timesheetParams.ClientId && (x.Date >= timesheetParams.FilterStart & x.Date <= timesheetParams.FilterEnd))
           .Include(x => x.Client)
           .Include(x => x.Project)
           .ThenInclude(x => x.Member)
           .Include(x => x.Category)
           .ToListAsync(cancellationToken))
           .Select(timesheet => new TimeSheet(
               timesheet.Id,
               timesheet.Description,
               timesheet.Time,
               timesheet.OverTime,
               timesheet.Date,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Project(timesheet.Project.Id, timesheet.Project.ProjectName, timesheet.Project.Description, timesheet.Project.Archive, timesheet.Project.Status,
               new Client(timesheet.Client.Id, timesheet.Client.ClientName, timesheet.Client.Adress, timesheet.Client.City, timesheet.Client.PostalCode, timesheet.Client.Country),
               new Member(timesheet.Project.Member.Id, timesheet.Project.Member.Name, timesheet.Project.Member.Username, timesheet.Project.Member.Email, timesheet.Project.Member.Hours, timesheet.Project.Member.Status, timesheet.Project.Member.Role, timesheet.Project.Member.Password)),
               new Category(timesheet.Category.Id, timesheet.Category.Name)
           ));
        } 

    }
    public async Task<IEnumerable<TimeSheet>> GetTimeSheetAsync(FetchParams fetchParams, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.TimeSheets
        .Where(x => fetchParams.Start <= x.Date & fetchParams.End >= x.Date)
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
        .ThenInclude( x => x.Member)
        .AsNoTracking()
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
        var persTS = await _dbContext.TimeSheets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == timesheet.Id);
        persTS.Id = timesheet.Id;
        persTS.Description = timesheet.Description;
        persTS.Time = timesheet.Time;
        persTS.OverTime = timesheet.OverTime;
        persTS.ClientId = timesheet.Client.Id;
        persTS.ProjectId = timesheet.Project.Id;
        persTS.CategoryId = timesheet.Category.Id;
    }
}
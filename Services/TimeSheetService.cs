using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions;
using System.Collections.Generic;
using Contracts;
using Domain;
using System.Linq;
using Contracts.Exceptions;
namespace Services
{
    internal sealed class TimeSheetService : ITimeSheetService
    {
      private readonly IRepositoryManager _repositoryManager;
      public TimeSheetService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
       public async Task<IEnumerable<TimeSheetDTO>> GetFilteredTS(TimeSheetParams timesheetParams, CancellationToken cancellationToken = default)
        {
            return (await _repositoryManager.TimeSheetRepository.GetFilteredTS(timesheetParams,cancellationToken)).Select(timesheet => new TimeSheetDTO()
            {
                Id = timesheet.Id.ToString(),
                Description = timesheet.Description,
                Time = timesheet.Time,
                OverTime = timesheet.OverTime,
                Date = timesheet.Date.ToShortDateString(),
                ClientId = timesheet.Client.Id.ToString(),
                ProjectId = timesheet.Project.Id.ToString(),
                CategoryId = timesheet.Category.Id.ToString(),
            }
          );
        }

        public async Task CreateAsync(TimeSheetDTO timesheetDTO, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = timesheetDTO.ClientId!= null?(await _repositoryManager.ClientRepository.GetByIdAsync(Guid.Parse(timesheetDTO.ClientId), cancellationToken)): null;
                var project = timesheetDTO.ProjectId!= null?(await _repositoryManager.ProjectRepository.GetProjectById(Guid.Parse(timesheetDTO.ProjectId), cancellationToken)): null;
                var category = timesheetDTO.CategoryId!= null?(await _repositoryManager.CategoryRepository.GetCategoryById(Guid.Parse(timesheetDTO.CategoryId), cancellationToken)): null;
            var domaintimesheet = new TimeSheet(Guid.NewGuid(),timesheetDTO.Description,timesheetDTO.Time,timesheetDTO.OverTime,new DateTime(), client, project, category);
            await _repositoryManager.TimeSheetRepository.InsertTimeSheet(domaintimesheet);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(EntityAlreadyExists)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
            var result = await _repositoryManager.TimeSheetRepository.GetTimeSheetById(id,cancellationToken);
            if(result == null)
            {
                throw new Exception("Time sheet not found");
            }
            _repositoryManager.TimeSheetRepository.RemoveTimeSheet(result);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(NotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CalendarTsDTO>> GetAllAsync(FetchParams fetchParams,CancellationToken cancellationToken = default)
        {
           return (await _repositoryManager.TimeSheetRepository.GetTimeSheetAsync(fetchParams,cancellationToken)).Select(timesheet => new CalendarTsDTO()
           {
               Id = timesheet.Id.ToString(),
               Description = timesheet.Description,
               Time = timesheet.Time,
               OverTime = timesheet.OverTime,
               Date = timesheet.Date,
               ClientId = timesheet.Client.Id.ToString(),
               ProjectId = timesheet.Project.Id.ToString(),
               CategoryId = timesheet.Category.Id.ToString(),
           }
           );
        }

        public async Task<TimeSheetDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
        try{
           var reuslt = await _repositoryManager.TimeSheetRepository.GetTimeSheetById(id,cancellationToken);
           var timeSheetDTO = new TimeSheetDTO()
           {
                Id = reuslt.Id.ToString(),
               Description = reuslt.Description,
               Time = reuslt.Time,
               OverTime = reuslt.OverTime,
               Date = reuslt.Date.ToShortDateString(),
               ClientId = reuslt.Client.Id.ToString(),
               ProjectId = reuslt.Project.Id.ToString(),
               CategoryId = reuslt.Category.Id.ToString(),
           };
           
           return timeSheetDTO;
        }
        catch(NotFoundException)
        {
            throw;
        } 
        catch(Exception)
        {
            throw;
        }
        }
        public async Task UpdateAsync(Guid id, TimeSheetDTO timeSheetDTO,CancellationToken cancellationToken = default)
        {
           try
           {
                var client = timeSheetDTO.ClientId != null ? (await _repositoryManager.ClientRepository.GetByIdAsync(Guid.Parse(timeSheetDTO.ClientId), cancellationToken)) : null;
                var project = timeSheetDTO.ProjectId != null ? (await _repositoryManager.ProjectRepository.GetProjectById(Guid.Parse(timeSheetDTO.ProjectId), cancellationToken)) : null;
                var category = timeSheetDTO.CategoryId != null ? (await _repositoryManager.CategoryRepository.GetCategoryById(Guid.Parse(timeSheetDTO.CategoryId), cancellationToken)) : null;
                var result = (await _repositoryManager.TimeSheetRepository.GetTimeSheetById(id))
                    .UpdateDescription(timeSheetDTO.Description)
                    .UpdateOvertime(timeSheetDTO.OverTime)
                    .UpdateTime(timeSheetDTO.Time)
                    .ProjectUpdate(project)
                    .CategoryUpdate(category)
                    .ClientUpdate(client);
           await _repositoryManager.TimeSheetRepository.UpdateTimeSheet(result,cancellationToken);
           await _repositoryManager.SaveChangesAsync(cancellationToken);
           }
           catch(NotFoundException)
           {
               throw;
           }
           catch(Exception)
           {
               throw;
           }
        }
    }
}

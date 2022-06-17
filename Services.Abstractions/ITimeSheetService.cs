using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain;

namespace Services.Abstractions
{
public interface ITimeSheetService
{
        Task<int> GetCount(TimeSheetParams timesheetParams);
        Task<IEnumerable<TimeSheetDTO>> GetFilteredTS(TimeSheetParams timesheetParams,CancellationToken cancellationToken = default);
        Task<IEnumerable<CalendarTsDTO>> GetAllAsync(FetchParams fetchParams,CancellationToken cancellationToken = default);
        Task<TimeSheetDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task CreateAsync(CreateTimeSheetDTO timesheetDTO,CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id,TimeSheetDTO timeSheetDTO,CancellationToken cancellationToken = default);
}

} 
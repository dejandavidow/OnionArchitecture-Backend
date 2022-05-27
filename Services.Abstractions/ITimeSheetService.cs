using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;

namespace Services.Abstractions
{
public interface ITimeSheetService
{
Task<IEnumerable<TimeSheetDTO>> GetAllAsync(CancellationToken cancellationToken = default);
Task<TimeSheetDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
Task CreateAsync(TimeSheetDTO timesheetDTO,CancellationToken cancellationToken = default);
Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
Task UpdateAsync(Guid id,TimeSheetDTO timeSheetDTO,CancellationToken cancellationToken = default);
}

} 
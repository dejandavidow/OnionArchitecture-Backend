using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
namespace Domain
{
    public interface ITimeSheetRepository
    {
        Task<IEnumerable<TimeSheet>> GetTimeSheetAsync(CancellationToken cancellationToken = default);
        Task<TimeSheet> GetTimeSheetById(Guid id,CancellationToken cancellationToken = default);
        Task InsertTimeSheet(TimeSheet timeSheet,CancellationToken cancellationToken = default);
        Task UpdateTimeSheet(TimeSheet timesheet,CancellationToken cancellationToken = default);
        void RemoveTimeSheet(TimeSheet timeSheet);
    }
}
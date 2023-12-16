using Contracts.DTOs;
using Contracts.Pagination;
using Domain.Entities;

namespace Services.Abstractions
{
    public interface ITimeSheetService
    {
        PaginatedList<TimeSheetDTO> GetAll(int pagenumber, int pagesize);
        TimeSheet GetOne(int id);
        void Create(TimeSheet timeSheet);
        void Update(TimeSheet timeSheet);
        void Delete(TimeSheet timeSheet);
    }


}
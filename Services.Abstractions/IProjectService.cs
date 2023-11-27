using Contracts.DTOs;
using Contracts.Pagination;
using Domain.Entities;

namespace Services.Abstractions
{
    public interface IProjectService
    {
        PaginatedList<ProjectDTO> GetAll(QueryParameters parameters);
        Project GetOne(int id);
        void Create(Project project);
        void Update(Project project);
        void Delete(Project project);
    }

}
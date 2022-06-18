using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Domain.Entities;
using Domain.Pagination;

namespace Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<int> CountSearchProjects(string search);
        Task<int> CountFilterProjects(string letter);
        Task<IEnumerable<Project>> SearchProjects(ProjectParams projectParams, string search);
        Task<IEnumerable<Project>> FilterProjects(ProjectParams projectParams, string letter);
        Task<IEnumerable<Project>> GetProjectAsync(ProjectParams projectParams, CancellationToken cancellationToken = default);
        Task<Project> GetProjectById(Guid id, CancellationToken cancellationToken = default);
        Task InsertProject(Project project, CancellationToken cancellationToken);
        Task UpdateProject(Project project, CancellationToken cancellationToken);
        void RemoveProject(Project project);
    }
}
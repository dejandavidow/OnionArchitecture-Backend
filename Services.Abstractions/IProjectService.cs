using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain;

namespace Services.Abstractions
{
public interface IProjectService
{
        Task<int> CountSearchProjects(string search);

        Task<int> CountFilterProjects(string letter);
        Task<IEnumerable<ProjectDTO>> SearchProjects(ProjectParams projectParams, string search);
        Task<IEnumerable<ProjectDTO>> FilterProjects(ProjectParams projectParams,string letter);
        Task<IEnumerable<ProjectDTO>> GetAllAsync(ProjectParams projectParams,CancellationToken cancellationToken = default);
        Task<ProjectDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task CreateAsync(ProjectDTO projectDTO,CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id,ProjectDTO projectDTO,CancellationToken cancellationToken = default);
}

} 
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
namespace Services.Abstractions
{
public interface IProjectService
{
Task<IEnumerable<ProjectDTO>> GetAllAsync(CancellationToken cancellationToken = default);
Task<ProjectDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
Task CreateAsync(ProjectDTO projectDTO,CancellationToken cancellationToken = default);
Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
Task UpdateAsync(Guid id,ProjectDTO projectDTO,CancellationToken cancellationToken = default);
}

} 
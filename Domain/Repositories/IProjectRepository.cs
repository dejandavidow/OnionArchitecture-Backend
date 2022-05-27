using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

public interface IProjectRepository
{
Task<IEnumerable<Project>> GetProjectAsync(CancellationToken cancellationToken = default);
Task<Project> GetProjectById(Guid id,CancellationToken cancellationToken = default);
Task InsertProject(Project project,CancellationToken cancellationToken);
Task UpdateProject(Project project,CancellationToken cancellationToken);
void RemoveProject(Project project);
}

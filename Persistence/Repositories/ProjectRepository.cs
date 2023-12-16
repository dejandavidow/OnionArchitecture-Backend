using Domain.Entities;
using Domain.Repositories;
using Persistence;
using Persistence.Repositories;

internal sealed class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    private readonly RepositoryDbContext _dbContext;
    public ProjectRepository(RepositoryDbContext dbContext) : base(dbContext)
    {
        _dbContext=dbContext;
    }
}
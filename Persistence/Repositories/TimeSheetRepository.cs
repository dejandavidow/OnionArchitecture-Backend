using Domain.Repositories;
using Persistence;


internal sealed class TimeSheetRepository : ITimeSheetRepository
{
    private readonly RepositoryDbContext _dbContext;
    public TimeSheetRepository(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
using Domain.Entities;
using Domain.Repositories;
using Persistence;
using Persistence.Repositories;


internal sealed class TimeSheetRepository : RepositoryBase<TimeSheet>, ITimeSheetRepository
{
    private readonly RepositoryDbContext _dbContext;
    public TimeSheetRepository(RepositoryDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
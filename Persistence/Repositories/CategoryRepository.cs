using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    internal sealed class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly RepositoryDbContext _dbContext;
        public CategoryRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

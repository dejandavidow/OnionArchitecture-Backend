using Domain.Entities;
using Domain.Repositories;
using Persistence;
using Persistence.Repositories;


internal sealed class ClientRepository : RepositoryBase<Client>, IClientRepository
{
    private readonly RepositoryDbContext _dbContext;
    public ClientRepository(RepositoryDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
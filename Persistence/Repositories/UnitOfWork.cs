using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private RepositoryDbContext _dbContext;
        private ICategoryRepository _categoryRepository;
        private IClientRepository _clientRepository;
        private IProjectRepository _projectRepository;
        private ITimeSheetRepository _timesheetRepository;
        public UnitOfWork(RepositoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_dbContext);
                }
                return _categoryRepository;
            }
        }
        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_dbContext);
                }
                return _clientRepository;
            }
        }
        public IProjectRepository ProjectRepository
        {
            get
            {
                if (_projectRepository == null)
                {
                    _projectRepository = new ProjectRepository(_dbContext);
                }
                return _projectRepository;
            }
        }
        public ITimeSheetRepository TimeSheetRepository
        {
            get
            {
                if (_timesheetRepository == null)
                {
                    _timesheetRepository = new TimeSheetRepository(_dbContext);
                }
                return _timesheetRepository;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
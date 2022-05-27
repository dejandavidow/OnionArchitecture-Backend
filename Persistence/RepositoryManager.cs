using Domain;
using Persistence;
using System.Threading.Tasks;
using System.Threading;

public sealed class RepositoryManager : IRepositoryManager
{
    private RepositoryDbContext _dbContext;
    private ICategoryRepository _categoryRepository;
    private IMemberRepository _memberRepository;
    private IClientRepository _clientRepository;
    private IProjectRepository _projectRepository;
    private ITimeSheetRepository _timesheetRepository;
    public RepositoryManager(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
     public ICategoryRepository CategoryRepository {
            get {
                if(_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_dbContext);
                }
                return _categoryRepository;
            }
        }
        public IClientRepository ClientRepository {
            get {
                if(_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_dbContext);
                }
                return _clientRepository;
            }
        }
        public IMemberRepository MemberRepository {
            get {
                if(_memberRepository == null)
                {
                    _memberRepository = new MemberRepository(_dbContext);
                }
                return _memberRepository;
            }
        }
        public IProjectRepository ProjectRepository {
            get {
                if(_projectRepository == null)
                {
                    _projectRepository = new ProjectRepository(_dbContext);
                }
                return _projectRepository;
            }
        }
        public ITimeSheetRepository TimeSheetRepository {
            get {
                if(_timesheetRepository == null)
                {
                    _timesheetRepository = new TimeSheetRepository(_dbContext);
                }
                return _timesheetRepository;
            }
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken){
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
}
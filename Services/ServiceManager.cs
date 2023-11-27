using Services.Abstractions;
namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        //private readonly Lazy<ICategoryService> _categoryService;
        //private readonly Lazy<IClientService> _clientService;
        //private readonly Lazy<IMemberService> _memberService;
        //private readonly Lazy<IProjectService> _projectService;
        //private readonly Lazy<ITimeSheetService> _timesheetService;
        //private readonly Lazy<IMailService> _mailService;
        //private readonly IMapper _mapper;
        //public ServiceManager(IUnitOfWork repositoryManager, IMailService mailService, IMapper mapper)
        //{
        //    _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, mapper));
        //    _clientService = new Lazy<IClientService>(() => new ClientService(repositoryManager));
        //    _memberService = new Lazy<IMemberService>(() => new MemberService(repositoryManager, mailService));
        //    _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager));
        //    _timesheetService = new Lazy<ITimeSheetService>(() => new TimeSheetService(repositoryManager));

        //}
        //public ICategoryService CategoryService => _categoryService.Value;
        //public IClientService ClientService => _clientService.Value;
        //public IMemberService MemberService => _memberService.Value;
        //public IProjectService ProjectService => _projectService.Value;
        //public ITimeSheetService TimeSheetService => _timesheetService.Value;
        //public IMailService MailService => _mailService.Value;
        //public IMapper Mapper => _mapper;
    }
}
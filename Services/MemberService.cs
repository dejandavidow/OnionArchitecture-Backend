using Domain.Repositories;
using Domain.Services;
using Services.Abstractions;
namespace Services
{
    internal sealed class MemberService : IMemberService
    {
        private readonly IUnitOfWork _repositoryManager;
        private readonly IMailService _mailService;

        public MemberService(IUnitOfWork repositoryManager, IMailService mailService)
        {
            _repositoryManager = repositoryManager;
            _mailService = mailService;
        }

    }
}
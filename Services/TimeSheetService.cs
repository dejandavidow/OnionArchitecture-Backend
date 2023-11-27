using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    public sealed class TimeSheetService : ITimeSheetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TimeSheetService(IUnitOfWork uow) => _unitOfWork = uow;

    }
}

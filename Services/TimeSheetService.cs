using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.DTOs;
using Contracts.Pagination;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;
using System.Linq;

namespace Services
{
    public sealed class TimeSheetService : ITimeSheetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TimeSheetService(IUnitOfWork uow, IMapper mapper)
        {
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public void Create(TimeSheet timeSheet)
        {
            _unitOfWork.TimeSheetRepository.Create(timeSheet);
            _unitOfWork.SaveChanges();
        }

        public void Delete(TimeSheet timeSheet)
        {
            _unitOfWork.TimeSheetRepository.Delete(timeSheet);
            _unitOfWork.SaveChanges();
        }

        public PaginatedList<TimeSheetDTO> GetAll(int pagenumber, int pagesize)
        {
            var times = _unitOfWork.TimeSheetRepository.FindAll().OrderByDescending(x => x.Date);
            var dtos = times.ProjectTo<TimeSheetDTO>(_mapper.ConfigurationProvider);
            return PaginatedList<TimeSheetDTO>.Create(dtos, pagenumber, pagesize);
        }

        public TimeSheet GetOne(int id)
        {
            return _unitOfWork.TimeSheetRepository.SingleById(x => x.Id == id);
        }

        public void Update(TimeSheet timeSheet)
        {
            _unitOfWork.TimeSheetRepository.Update(timeSheet);
            _unitOfWork.SaveChanges();
        }
    }
}

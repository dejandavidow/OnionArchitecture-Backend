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
    public sealed class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(Project project)
        {
            _unitOfWork.ProjectRepository.Create(project);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Project project)
        {
            _unitOfWork.ProjectRepository?.Delete(project);
            _unitOfWork.SaveChanges();
        }

        public PaginatedList<ProjectDTO> GetAll(QueryParameters parameters)
        {
            var projects = _unitOfWork.ProjectRepository.FindAll();
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                projects = projects.Where(x => x.Name.Contains(parameters.Search));
            }
            if (!string.IsNullOrEmpty(parameters.Letter))
            {
                projects = projects.Where(x => x.Name.StartsWith(parameters.Letter));
            }
            var sort = string.IsNullOrEmpty(parameters.OrderBy) ? "name" : parameters.OrderBy;
            switch (sort)
            {
                case "name":
                    projects = projects.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    projects = projects.OrderByDescending(x => x.Name);
                    break;
                default:
                    projects = projects.OrderBy(x => x.Name);
                    break;
            }
            var dtos = projects.ProjectTo<ProjectDTO>(_mapper.ConfigurationProvider);
            return PaginatedList<ProjectDTO>.Create(dtos, parameters.PageNumber, parameters.PageSize);
        }

        public Project GetOne(int id)
        {
            return _unitOfWork.ProjectRepository.SingleById(x => x.Id == id);
        }

        public void Update(Project project)
        {
            _unitOfWork.ProjectRepository.Update(project);
            _unitOfWork.SaveChanges();
        }
    }
}
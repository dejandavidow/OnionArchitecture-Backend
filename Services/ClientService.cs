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
    public sealed class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PaginatedList<ClientDTO> GetAll(QueryParameters parameters)
        {
            var clients = _unitOfWork.ClientRepository.FindAll();
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                clients = clients.Where(x => x.Name.Contains(parameters.Search));
            }
            if (!string.IsNullOrEmpty(parameters.Letter))
            {
                clients = clients.Where(x => x.Name.StartsWith(parameters.Letter));
            }
            var sort = string.IsNullOrEmpty(parameters.OrderBy) ? "name" : parameters.OrderBy;
            switch (sort)
            {
                case "name":
                    clients = clients.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    clients = clients.OrderByDescending(x => x.Name);
                    break;
                default:
                    clients = clients.OrderBy(x => x.Name);
                    break;
            }
            var dtos = clients.ProjectTo<ClientDTO>(_mapper.ConfigurationProvider);
            return PaginatedList<ClientDTO>.Create(dtos, parameters.PageNumber, parameters.PageSize);
        }
        public void Create(Client client)
        {
            _unitOfWork.ClientRepository.Create(client);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Client client)
        {
            _unitOfWork.ClientRepository.Delete(client);
            _unitOfWork.SaveChanges();
        }

        public Client GetOne(int id)
        {
            return _unitOfWork.ClientRepository.SingleById(x => x.Id == id);
        }

        public void Update(Client client)
        {
            _unitOfWork.ClientRepository.Update(client);
            _unitOfWork.SaveChanges();
        }
    }
}
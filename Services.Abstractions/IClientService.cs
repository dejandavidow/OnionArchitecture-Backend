using Contracts.DTOs;
using Contracts.Pagination;
using Domain.Entities;

namespace Services.Abstractions
{
    public interface IClientService
    {
        PaginatedList<ClientDTO> GetAll(QueryParameters parameters);
        Client GetOne(int id);
        void Create(Client client);
        void Update(Client client);
        void Delete(Client client);
    }

}
using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions;
using System.Collections.Generic;
using Domain;
using Contracts;
using System.Linq;
using Contracts.Exceptions;

namespace Services
{
    internal sealed class ClientService : IClientService
    {
        private readonly IRepositoryManager _repositoryManager;
        public ClientService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
       public async Task<int> GetFilterCount(string letter)
        {
            return await _repositoryManager.ClientRepository.GetFilterCount(letter);
        }
        public async Task<IEnumerable<ClientDTO>> GetFilterAsync(ClientParams clientParams, CancellationToken cancellationToken = default)
        {
            var clientdto = (await _repositoryManager.ClientRepository.GetFilterAsync(clientParams, cancellationToken)).Select(client => new ClientDTO
            {
                Id = client.Id.ToString(),
                ClientName = client.ClientName,
                Adress = client.Adress,
                City = client.City,
                PostalCode = client.PostalCode,
                Country = client.Country
            });
            return clientdto;
        }
        public async Task<int> GetAllCount(string search)
        {
           return await _repositoryManager.ClientRepository.GetClientCount(search);
        }

        public async Task CreateAsync(ClientDTO clientDTO, CancellationToken cancellationToken = default)
        {
            try
            {
            var ClientModel = new Client(Guid.NewGuid(), clientDTO.ClientName, clientDTO.Adress, clientDTO.City, clientDTO.PostalCode, clientDTO.Country); 
            await _repositoryManager.ClientRepository.InsertClient(ClientModel, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(EntityAlreadyExists)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try{
            var result = await _repositoryManager.ClientRepository.GetByIdAsync(id,cancellationToken);
            _repositoryManager.ClientRepository.RemoveClient(result);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(NotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync(ClientParams clientParams,CancellationToken cancellationToken = default)
        {
            var clientdto = (await _repositoryManager.ClientRepository.GetAllAsync(clientParams,cancellationToken)).Select(client => new ClientDTO
            {
                Id = client.Id.ToString(),
                ClientName = client.ClientName,
                Adress = client.Adress,
                City= client.City,
                PostalCode = client.PostalCode,
                Country = client.Country
            });
            return clientdto;
        }

        public async Task<IEnumerable<ClientDTO>> GetSearchAsync(ClientParams clientParams,string name,CancellationToken cancellationToken = default)
        {
            var result = (await _repositoryManager.ClientRepository.GetSearchAsync(clientParams,name,cancellationToken)).Select(client => new ClientDTO() {
            Id = client.Id.ToString(),
            Adress = client.Adress,
            City = client.City,
            ClientName = client.ClientName,
            Country = client.Country,
            PostalCode = client.PostalCode
        });;
            return result;
        }

        public async Task<ClientDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _repositoryManager.ClientRepository.GetByIdAsync(id,cancellationToken);
                var clientDTO = new ClientDTO()
                {
                Id = result.Id.ToString(),
                Adress = result.Adress,
                City = result.City,
                ClientName = result.ClientName,
                Country = result.Country,
                PostalCode = result.PostalCode,
                };
                return clientDTO;
            }
            catch(NotFoundException)
            {
            throw;
            }
            catch(Exception)
            {
            throw;
            }
        }
            
        public async Task UpdateAsync(Guid id,ClientDTO clientDTO, CancellationToken cancellationToken = default)
        {
            try{
            var result = (await _repositoryManager.ClientRepository.GetByIdAsync(id,cancellationToken)).
            UpdateName(clientDTO.ClientName).UpdateAddress(clientDTO.Adress).
            UpdateCity(clientDTO.City).UpdateCountry(clientDTO.Country).UpdatePostal(clientDTO.PostalCode);
            await _repositoryManager.ClientRepository.UpdateClient(result, cancellationToken);
             await _repositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch(NotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
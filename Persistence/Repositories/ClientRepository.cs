
using Contracts.Exceptions;
using Domain.Entities;
using Domain.Pagination;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


internal sealed class ClientRepository : IClientRepository
{
    private readonly RepositoryDbContext _dbContext;
    public ClientRepository(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> GetFilterCount(string letter)
    {
        return await _dbContext.Clients.Where(x => x.ClientName.StartsWith(letter)).CountAsync();
    }

    public async Task<int> GetClientCount(string search)
    {
        if (!String.IsNullOrEmpty(search))
        {
            return await _dbContext.Clients.Where(x => x.ClientName.Contains(search)).CountAsync();
        }
        else
        {
            return await _dbContext.Clients.CountAsync();
        }
       
    }

    public async Task<IEnumerable<Client>> GetFilterAsync(ClientParams clientParams,CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Clients.Where(x => x.ClientName.StartsWith(clientParams.letter)).AsNoTracking()
        .OrderBy(x => x.ClientName)
        .Skip((clientParams.PageNumber - 1) * clientParams.PageSize)
        .Take(clientParams.PageSize)
        .ToListAsync(cancellationToken))
        .Select(client => new Client(client.Id, client.ClientName, client.Adress, client.City, client.PostalCode, client.Country));
    }

    public async Task<IEnumerable<Client>> GetSearchAsync(ClientParams clientParams,string name,CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Clients.Where(x => x.ClientName.Contains(name)).AsNoTracking()
        .OrderBy(x => x.ClientName)
        .Skip((clientParams.PageNumber - 1) * clientParams.PageSize)
        .Take(clientParams.PageSize)
        .ToListAsync(cancellationToken))
        .Select(client => new Client(client.Id, client.ClientName, client.Adress, client.City, client.PostalCode, client.Country));
    }
    public async Task<Client> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var client = await _dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        if(client == null)
        {
            throw new NotFoundException("Client not found");
            //return null;
        }
        return new Client(client.Id, client.ClientName, client.Adress, client.City, client.PostalCode, client.Country);
    }

    public async Task InsertClient(Client client,CancellationToken cancellationToken)
    {
        var existclient = await _dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == client.Id,cancellationToken);
        if(existclient != null)
        {
            throw new EntityAlreadyExists("Client already exists.");
        }
        else
        {
         _dbContext.Clients.Add(new Persistence.Models.PersistenceClient(){
            Id = client.Id,
            Adress = client.Adress,
            City = client.City,
            ClientName = client.ClientName,
            Country = client.Country,
            PostalCode = client.PostalCode
        });
        }
    }

    public void RemoveClient(Client client)
    {
        _dbContext.Clients.Remove(new Persistence.Models.PersistenceClient(){
            Id = client.Id,
            Adress = client.Adress,
            City = client.City,
            ClientName = client.ClientName,
            Country = client.Country,
            PostalCode = client.PostalCode
        });
    }

    public async Task UpdateClient(Client client, CancellationToken cancellationToken){
        var persistenceClient = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id,cancellationToken);
        persistenceClient.Id = client.Id;
        persistenceClient.Adress = client.Adress;
        persistenceClient.City = client.City;
        persistenceClient.ClientName = client.ClientName;
        persistenceClient.Country = client.Country;
        persistenceClient.PostalCode = client.PostalCode;
    }

    public async Task<IEnumerable<Client>> GetAllAsync(ClientParams clientParams,CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Clients.AsNoTracking()
            .OrderBy(x => x.ClientName)
            .Skip((clientParams.PageNumber - 1) * clientParams.PageSize)
            .Take(clientParams.PageSize)
            .ToListAsync(cancellationToken))
            .Select(client => new Client(client.Id, client.ClientName, client.Adress, client.City, client.PostalCode, client.Country));
    }
}
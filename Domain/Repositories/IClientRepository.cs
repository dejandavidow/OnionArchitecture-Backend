using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using System.Collections;
public interface IClientRepository
{
    Task<int> GetClientCount(string search);
    Task<int> GetFilterCount(string letter);
    Task<IEnumerable<Client>> GetFilterAsync(ClientParams clientParams, CancellationToken cancellationToken = default);
    Task<IEnumerable<Client>> GetAllAsync(ClientParams clientParams,CancellationToken cancellationToken = default);
    Task<IEnumerable<Client>> GetSearchAsync(ClientParams clientParams,string name, CancellationToken cancellationToken = default);
    Task<Client> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    void RemoveClient(Client client);
    Task InsertClient(Client client, CancellationToken cancellationToken);
    Task UpdateClient(Client client, CancellationToken cancellationToken);
}
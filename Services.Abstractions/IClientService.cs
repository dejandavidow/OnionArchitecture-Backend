using Contracts.DTOs;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Abstractions
{
public interface IClientService
{
Task<int> GetAllCount(string search);
Task<int> GetFilterCount(string letter);
Task<IEnumerable<ClientDTO>> GetFilterAsync(ClientParams clientParams,CancellationToken cancellationToken = default);
Task<IEnumerable<ClientDTO>> GetSearchAsync(ClientParams clientParams,string name,CancellationToken cancellationToken = default);
Task<IEnumerable<ClientDTO>> GetAllAsync(ClientParams clientParams,CancellationToken cancellationToken = default);
Task<ClientDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
Task CreateAsync(ClientDTO clientDTO,CancellationToken cancellationToken = default);
Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
Task UpdateAsync(Guid id, ClientDTO clientDTO, CancellationToken cancellationToken = default);
}

} 
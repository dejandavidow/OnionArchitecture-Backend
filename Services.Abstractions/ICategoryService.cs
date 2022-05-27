using Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Services.Abstractions
{
public interface ICategoryService
{
Task<IEnumerable<CategoryDTO>> GetAllAsync(CancellationToken cancellationToken = default);
Task<CategoryDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
Task CreateAsync(CategoryDTO categoryDTO,CancellationToken cancellationToken = default);
Task DeleteAsync(Guid Id,CancellationToken cancellationToken = default);
Task UpdateAsync(Guid Id,CategoryDTO categoryDTO,CancellationToken cancellationToken = default);
}

} 
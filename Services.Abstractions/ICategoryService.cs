using Contracts.DTOs;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Services.Abstractions
{
public interface ICategoryService
{
        Task<int> searchCountAsync(string search);
        Task<int> FilterCountAsync(string letter);
        Task<IEnumerable<CategoryDTO>> FilterAsync(CategoryParams categoryParams, string letter);
        Task<IEnumerable<CategoryDTO>> SearchAsync(CategoryParams categoryParams,string search);
        Task<IEnumerable<CategoryDTO>> GetAllAsync(CategoryParams categoryParams,CancellationToken cancellationToken = default);
        Task<CategoryDTO> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task CreateAsync(CategoryDTO categoryDTO,CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid Id,CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid Id,CategoryDTO categoryDTO,CancellationToken cancellationToken = default);

}

} 
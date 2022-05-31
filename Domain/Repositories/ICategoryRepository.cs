using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

public interface ICategoryRepository
{
    Task<int> FilterCountAsync(string letter);
    Task<IEnumerable<Category>> FilterAsync(CategoryParams categoryParams, string letter);
    Task<IEnumerable<Category>> SearchAsync(CategoryParams categoryParams,string search);
Task<IEnumerable<Category>> GetCateroriesAsync(CategoryParams categoryParams,CancellationToken cancellationToken = default);
Task<Category> GetCategoryById(Guid id,CancellationToken cancellationToken = default);
void RemoveCategory(Category category);
Task InsertCategory(Category category,CancellationToken cancellationToken = default);
Task UpdateCategory(Category category,CancellationToken cancellationToken);
}
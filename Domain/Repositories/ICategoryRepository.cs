using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

public interface ICategoryRepository
{
Task<IEnumerable<Category>> GetCateroriesAsync(CancellationToken cancellationToken = default);
Task<Category> GetCategoryById(Guid id,CancellationToken cancellationToken = default);
void RemoveCategory(Category category);
Task InsertCategory(Category category,CancellationToken cancellationToken = default);
Task UpdateCategory(Category category,CancellationToken cancellationToken);
}

using Contracts.Exceptions;
using Domain.Entities;
using Domain.Pagination;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class CategoryRepository : ICategoryRepository
    {
        private readonly RepositoryDbContext _dbContext;
        public CategoryRepository(RepositoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> searchCountAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dbContext.Categories.CountAsync();
            }
            return _dbContext.Categories.Where(x => x.Name.StartsWith(search)).CountAsync();
        }
        public Task<int> FilterCountAsync(string letter)
        {
            return _dbContext.Categories.Where(x => x.Name.StartsWith(letter)).CountAsync();
        }
        public async Task<IEnumerable<Category>> FilterAsync(CategoryParams categoryParams, string letter)
        {
            return (await _dbContext.Categories.AsNoTracking().Where(x => x.Name.StartsWith(letter))
              .OrderBy(x => x.Name)
              .Skip((categoryParams.PageNumber - 1) * categoryParams.PageSize)
              .Take(categoryParams.PageSize)
              .ToListAsync())
              .Select(category => new Category(category.Id, category.Name));
        }
        public async Task<IEnumerable<Category>> SearchAsync(CategoryParams categoryParams, string search)
        {
            return (await _dbContext.Categories.AsNoTracking().Where(x => x.Name.Contains(search))
               .OrderBy(x => x.Name)
               .Skip((categoryParams.PageNumber - 1) * categoryParams.PageSize)
               .Take(categoryParams.PageSize)
               .ToListAsync())
               .Select(category => new Category(category.Id, category.Name));
        }
        public async Task<Category> GetCategoryById(Guid id, CancellationToken cancellationToken = default)
        {
            var domaincategory = await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
           if (domaincategory == null)
            {
                throw new NotFoundException("Category not found");
            }
            return new Category(domaincategory.Id, domaincategory.Name);
        }

        public async Task<IEnumerable<Category>> GetCateroriesAsync(CategoryParams categoryParams, CancellationToken cancellationToken = default)
        {
            return (await _dbContext.Categories.AsNoTracking()
               .OrderBy(x => x.Name)
               .Skip((categoryParams.PageNumber - 1) * categoryParams.PageSize)
               .Take(categoryParams.PageSize)
               .ToListAsync(cancellationToken))
               .Select(category => new Category(category.Id, category.Name));
        }

        public async Task InsertCategory(Category category, CancellationToken cancellationToken = default)
        {
            var checkcategory = await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);
            if (checkcategory != null)
            {
                throw new EntityAlreadyExists("Entity already exists");
            }
            await _dbContext.Categories.AddAsync(new Models.PersistenceCategory()
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        public void RemoveCategory(Category category)
        {
            _dbContext.Categories.Remove(new Models.PersistenceCategory()
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        public async Task UpdateCategory(Category category, CancellationToken cancellationToken)
        {
            var persistencecategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);
            persistencecategory.Id = category.Id;
            persistencecategory.Name = category.Name;
        }
    }
}
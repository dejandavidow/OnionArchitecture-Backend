using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Contracts.Exceptions;
internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly RepositoryDbContext _dbContext;
    public CategoryRepository(RepositoryDbContext dbContext)
    {
        _dbContext=dbContext;
    }

    public async Task<Category> GetCategoryById(Guid id, CancellationToken cancellationToken = default)
    {
        var domaincategory = await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        if(domaincategory == null)
        {
            throw new NotFoundException("Category not found");
        }
        return new Category(domaincategory.Id,domaincategory.Name);
    }

    public async Task<IEnumerable<Category>> GetCateroriesAsync(CancellationToken cancellationToken = default)
    {
        var domaincategories = (await _dbContext.Categories.AsNoTracking().ToListAsync(cancellationToken)).Select( category => new Category(category.Id,category.Name));
        return domaincategories;
    }

    public async Task InsertCategory(Category category, CancellationToken cancellationToken = default)
    {
        var checkcategory = await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == category.Id,cancellationToken);
        if(checkcategory != null)
        {
            throw new EntityAlreadyExists("Entity already exists");
        }
        await _dbContext.Categories.AddAsync(new Persistence.Models.PersistenceCategory()
        {
            Id = category.Id,
            Name = category.Name
        });
    }

    public void RemoveCategory(Category category)
    {
        _dbContext.Categories.Remove(new Persistence.Models.PersistenceCategory()
        {
            Id = category.Id,
            Name = category.Name
        });
    }

    public async Task UpdateCategory(Category category, CancellationToken cancellationToken)
    {
        var persistencecategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id,cancellationToken);
        persistencecategory.Id = category.Id;
        persistencecategory.Name = category.Name;
    }
}
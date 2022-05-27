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
    internal sealed class CategoryService : ICategoryService
    {
        public IRepositoryManager _repositoryManager;
        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task CreateAsync(CategoryDTO categoryDTO, CancellationToken cancellationToken = default)
        {
            try
            {
            var domaincategory = new Category(Guid.NewGuid(),categoryDTO.Name);
            await _repositoryManager.CategoryRepository.InsertCategory(domaincategory,cancellationToken);
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
        public async Task DeleteAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            try
            {
            var categorytodelete = await _repositoryManager.CategoryRepository.GetCategoryById(Id,cancellationToken);
            _repositoryManager.CategoryRepository.RemoveCategory(categorytodelete);
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

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categoriesdto = (await _repositoryManager.CategoryRepository.GetCateroriesAsync(cancellationToken)).Select( category => new CategoryDTO(){
            Id = category.Id.ToString(),
            Name = category.Name
            });
            return categoriesdto;
        }

        public async Task<CategoryDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var categorydomain = await _repositoryManager.CategoryRepository.GetCategoryById(id,cancellationToken);
            var categorydto = new CategoryDTO()
            {
                Id = categorydomain.Id.ToString(),
                Name = categorydomain.Name
            };
            return categorydto;
        }

        public async Task UpdateAsync(Guid id,CategoryDTO categoryDTO, CancellationToken cancellationToken = default)
        {
            try
            {
            var categorytoupdate = (await _repositoryManager.CategoryRepository.GetCategoryById(id,cancellationToken)).UpdateCategoryName(categoryDTO.Name);
            await _repositoryManager.CategoryRepository.UpdateCategory(categorytoupdate,cancellationToken);
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
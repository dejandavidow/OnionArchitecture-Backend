

using Contracts.DTOs;
using Contracts.Exceptions;
using Domain.Entities;
using Domain.Pagination;
using Domain.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Services
{
    internal sealed class CategoryService : ICategoryService
    {
        public IRepositoryManager _repositoryManager;
        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<int> searchCountAsync(string search)
        {
            return await _repositoryManager.CategoryRepository.searchCountAsync(search);
        }
        public async Task<int> FilterCountAsync(string letter)
        {
            return await _repositoryManager.CategoryRepository.FilterCountAsync(letter);
        }
        public async Task<IEnumerable<CategoryDTO>> FilterAsync(CategoryParams categoryParams, string letter)
        {
            var categoriesdto = (await _repositoryManager.CategoryRepository.FilterAsync(categoryParams,letter))
                .Select(category => new CategoryDTO()
                {
                    Id = category.Id.ToString(),
                    Name = category.Name
                });
            return categoriesdto;
        }
        public async Task<IEnumerable<CategoryDTO>> SearchAsync(CategoryParams categoryParams, string search)
        {
            var categoriesdto = (await _repositoryManager.CategoryRepository.SearchAsync(categoryParams,search))
                .Select(category => new CategoryDTO()
            {
                Id = category.Id.ToString(),
                Name = category.Name
            });
            return categoriesdto;
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

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(CategoryParams categoryParams,CancellationToken cancellationToken = default)
        {
            var categoriesdto = (await _repositoryManager.CategoryRepository.GetCateroriesAsync(categoryParams,cancellationToken)).Select( category => new CategoryDTO(){
            Id = category.Id.ToString(),
            Name = category.Name
            });
            return categoriesdto;
        }

        public async Task<CategoryDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var categorydomain = await _repositoryManager.CategoryRepository.GetCategoryById(id, cancellationToken);
                var categorydto = new CategoryDTO()
                {
                    Id = categorydomain.Id.ToString(),
                    Name = categorydomain.Name
                };
                return categorydto;
            }
            catch(NotFoundException)
            {
                throw;
            }
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
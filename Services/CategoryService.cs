using Contracts.Pagination;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;
using System.Linq;
namespace Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PaginatedList<Category> GetAll(QueryParameters parameters)
        {
            var categories = _unitOfWork.CategoryRepository.FindAll();
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                categories = categories.Where(x => x.Name.Contains(parameters.Search));
            }
            if (!string.IsNullOrEmpty(parameters.Letter))
            {
                categories = categories.Where(x => x.Name.StartsWith(parameters.Letter));
            }
            var sort = string.IsNullOrEmpty(parameters.OrderBy) ? "name" : parameters.OrderBy;
            switch (sort)
            {
                case "name":
                    categories = categories.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    categories = categories.OrderByDescending(x => x.Name);
                    break;
                default:
                    categories = categories.OrderBy(x => x.Name);
                    break;
            }
            return PaginatedList<Category>.Create(categories, parameters.PageNumber, parameters.PageSize);
        }
        public void Create(Category category)
        {
            _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Category category)
        {
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.SaveChanges();
        }

        public Category GetOne(int id)
        {
            return _unitOfWork.CategoryRepository.SingleById(x => x.Id == id);
        }

        public void Update(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.SaveChanges();
        }
    }
}
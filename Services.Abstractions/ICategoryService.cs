using Contracts.Pagination;
using Domain.Entities;

namespace Services.Abstractions
{
    public interface ICategoryService
    {
        PaginatedList<Category> GetAll(QueryParameters parameters);
        Category GetOne(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(Category category);

    }

}
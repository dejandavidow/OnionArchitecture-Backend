using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    internal class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryDbContext _dbContext;
        public RepositoryBase(RepositoryDbContext context)
        {

            _dbContext=context;

        }
        public IQueryable<T> FindAll() => _dbContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _dbContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _dbContext.Set<T>().Add(entity);
        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public T SingleById(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().AsNoTracking().SingleOrDefault(expression);
        }
    }
}

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Task_Manager_Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T entity);
        public Task Delete(T entity);
        public Task Update(T entity);
        public Task<T?> FindById(int Id);
        public Task<T?> FindByName( string Name);
        public Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
    }
}

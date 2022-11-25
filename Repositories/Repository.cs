using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Task_Manager_Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MainDbContext context;
        private DbSet<T> DbSet;


        public Repository(MainDbContext _context)
        {
            this.context = _context;
            DbSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public async Task Add(T entity)
        {
            DbSet.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            DbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        public async Task Delete(T entity)
        {
            DbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<T?> FindById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T?> FindByName(string Name)
        {
            return await DbSet.FindAsync(Name);
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return  await DbSet.Where(expression).ToListAsync();
        }
    }
}

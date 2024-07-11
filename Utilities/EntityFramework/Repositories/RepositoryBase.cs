using LocationApp.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LocationApp.Utilities.EntityFramework.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly LocationContext _locationContext;
        private readonly DbSet<T> dbSet;

        public RepositoryBase(LocationContext locationContext)
        {
            _locationContext = locationContext;
            dbSet = _locationContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet?.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            var entity =dbSet.FirstOrDefault(filter);
            return entity;
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}

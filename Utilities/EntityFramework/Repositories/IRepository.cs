using LocationApp.Entities.Abstract;
using System.Linq.Expressions;

namespace LocationApp.Utilities.EntityFramework.Repositories
{
    public interface IRepository<T> where T : class, IEntity ,new()
    {
        List<T> GetAll();

        T Get(Expression<Func<T,bool>> filter);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}

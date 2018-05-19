using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GM.DAL.Infrastructure
{
    public interface IGenericRepository<T> : IDisposable where T : IEntity
    {

        Task<IEnumerable<T>> GetAll();

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        Task<T> Find(long id);

        Task Add(T entity, bool force = true);

        Task Delete(long id, bool force = true);

        Task Update(T entity, bool force = true);

        Task SaveChanges();

    }
}
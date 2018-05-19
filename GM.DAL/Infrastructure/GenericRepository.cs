using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GM.DAL.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _unitOfWork.Context.Set<T>().ToArrayAsync();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context.Set<T>().Where(predicate);
        }

        public async Task<T> Find(long id)
        {
            return await _unitOfWork.Context.Set<T>().FindAsync(id);
        }

        public async Task Add(T t, bool force = true)
        {
            _unitOfWork.Context.Set<T>().Add(t);
            _unitOfWork.Context.Entry(t).State = EntityState.Added;

            if (force)
                await SaveChanges();
        }

        public async Task Update(T t, bool force = true)
        {
            _unitOfWork.Context.Set<T>().Attach(t);
            _unitOfWork.Context.Entry(t).State = EntityState.Modified;

            if (force)
                await SaveChanges();
        }

        public async Task Delete(long id, bool force = true)
        {
            var t = await _unitOfWork.Context.Set<T>().FindAsync(id);

            _unitOfWork.Context.Set<T>().Remove(t);
            _unitOfWork.Context.Entry(t).State = EntityState.Deleted;

            if (force)
                await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }

}

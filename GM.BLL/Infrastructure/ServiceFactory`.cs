using System;
using System.Collections.Generic;
using System.Text;
using GM.DAL.Infrastructure;

namespace GM.BLL.Infrastructure
{
    public abstract class ServiceFactory<T> : IServiceFactory<T> where T : BaseEntity
    {
        protected ServiceFactory(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = new GenericRepository<T>(unitOfWork);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }

        protected IUnitOfWork UnitOfWork { get; }

        public IGenericRepository<T> Repository { get; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using GM.DAL.Infrastructure;

namespace GM.BLL.Infrastructure
{
    public abstract class ServiceFactory : IServiceFactory
    {
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        protected IUnitOfWork UnitOfWork { get; }

        protected ServiceFactory(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

    }
}

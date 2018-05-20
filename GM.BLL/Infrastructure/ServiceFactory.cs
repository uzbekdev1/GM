using GM.DAL.Infrastructure;

namespace GM.BLL.Infrastructure
{
    public abstract class ServiceFactory : IServiceFactory
    {
        protected ServiceFactory(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; } 

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

    }
}
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

        protected IUnitOfWork UnitOfWork { get; }

        public void Dispose()
        {
            Repository.Dispose();
        }

        public IGenericRepository<T> Repository { get; }
    }
}
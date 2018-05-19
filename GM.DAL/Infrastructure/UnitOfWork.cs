using System.Threading.Tasks;

namespace GM.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        public ApplicationDbContext Context { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }

}

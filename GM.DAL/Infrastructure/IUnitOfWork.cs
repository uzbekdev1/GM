using System;
using System.Threading.Tasks;

namespace GM.DAL.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {

        ApplicationDbContext Context { get; }

        Task Commit(); 

    }
}
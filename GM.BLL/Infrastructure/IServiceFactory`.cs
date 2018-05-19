using System;
using System.Collections.Generic;
using System.Text;
using GM.DAL.Infrastructure;

namespace GM.BLL.Infrastructure
{
    public interface IServiceFactory<T> : IServiceFactory where T : IEntity
    {

        IGenericRepository<T> Repository { get; }

    }
}

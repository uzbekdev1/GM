using System;
using System.Collections.Generic;
using System.Text;
using GM.DAL.Infrastructure;

namespace GM.BLL.Extension
{
    public static class ServiceExtension
    {

        public static IGenericRepository<T> GetRepository<T>(this IUnitOfWork unitOfWork) where T : BaseEntity
        {
            return new GenericRepository<T>(unitOfWork);
        }
    }
}

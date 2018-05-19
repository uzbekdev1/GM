using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;

namespace GM.BLL.Services
{
    public interface IPlayerService: IServiceFactory
    {

        Task<PlayerState> GetState(string name);

    }
}

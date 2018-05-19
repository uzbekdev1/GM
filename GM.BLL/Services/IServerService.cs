using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;
using GM.DAL.Entity;

namespace GM.BLL.Services
{
    public interface IServerService : IServiceFactory
    {

        Task<IEnumerable<ServerInfo>> GetInfos();

        Task<Matche> GetMatche(string endpoint);

        Task<ServerInfo> GetInfo(string endpoint);

        Task<ServerState> GetState(string endpoint);

    }
}

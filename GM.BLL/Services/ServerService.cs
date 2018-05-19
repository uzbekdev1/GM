using System.Collections.Generic;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;
using GM.DAL.Entity;
using GM.DAL.Infrastructure;

namespace GM.BLL.Services
{
    public class ServerService : ServiceFactory<Server>, IServerService
    {

        public ServerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<ServerInfo> GetInfo(string endpoint)
        {
            return Task.FromResult(new ServerInfo());
        }

        public Task<IEnumerable<ServerInfo>> GetInfos()
        {
            return Task.FromResult<IEnumerable<ServerInfo>>(new ServerInfo[0]);
        }

        public Task<Matche> GetMatche(string endpoint)
        {
            return Task.FromResult(new Matche());
        }

        public Task<ServerState> GetState(string endpoint)
        {
            return Task.FromResult(new ServerState());
        }

    }
}
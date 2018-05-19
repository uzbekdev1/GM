using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;
using GM.DAL.Entity;
using GM.DAL.Infrastructure;

namespace GM.BLL.Services
{
    public class PlayerService : ServiceFactory<Player>, IPlayerService
    {
        public PlayerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<PlayerState> GetState(string name)
        {
            return Task.FromResult(new PlayerState());
        }
    }
}
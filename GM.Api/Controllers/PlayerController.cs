using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Services;
using GM.DAL.Entity;
using GM.DAL.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GM.Api.Controllers
{
    [Route("players")]
    public class PlayerController : Controller
    {
        private readonly IGenericRepository<Player> _repository;
        private readonly IPlayerService _service;

        public PlayerController(IGenericRepository<Player> repository, IPlayerService service)
        {
            _repository = repository;
            _service = service;
        }

        // <name>/stats GET
        [HttpGet("{name:length(10,50)}/stats")]
        public async Task<PlayerState> GetStates(string name)
        {
            return await _service.GetState(name);
        }
    }
}
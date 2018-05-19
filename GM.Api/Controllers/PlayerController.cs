using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Services;
using GM.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using GM.DAL.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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

        // players/<name>/stats GET
        [HttpGet("{name}/stats")]
        public async Task<PlayerState> GetStates(string name)
        {
            return await _service.GetState(name);
        }

    }
}

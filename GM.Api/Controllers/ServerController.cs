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
    [Route("servers")]
    public class ServerController : Controller
    {
        private readonly IGenericRepository<Server> _repository;
        private readonly IServerService _service;

        public ServerController(IGenericRepository<Server> repository, IServerService service)
        {
            _repository = repository;
            _service = service;
        }

        // servers/info GET 
        [HttpGet("info")]
        public async Task<IEnumerable<ServerInfo>> GetInfos()
        {
            return await _service.GetInfos();
        }

        // servers/<endpoint>/info  GET
        [HttpGet("info/{endpoint}")]
        public async Task<ServerInfo> GetInfo(string endpoint)
        {
            return await _service.GetInfo(endpoint);
        }

        // servers/<endpoint>/info PUT 
        [HttpPut("info/{endpoint}")]
        public async Task<ServerInfo> PutInfo(string endpoint)
        {
            return await _service.GetInfo(endpoint);
        }

    }
}

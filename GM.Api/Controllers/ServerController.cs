using System.Collections.Generic;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Services;
using GM.DAL.Entity;
using GM.DAL.Infrastructure;
using Microsoft.AspNetCore.Mvc;

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

        // info GET 
        [HttpGet("info")]
        public async Task<IEnumerable<ServerInfo>> GetInfos()
        {
            return await _service.GetInfos();
        }

        // <endpoint>/info  GET
        [HttpGet("{endpoint}/info")]
        public async Task<ServerInfo> GetInfo(string endpoint)
        {
            return await _service.GetInfo(endpoint);
        }

        // <endpoint>/info PUT 
        [HttpPut("{endpoint}/info")]
        public async Task<ServerInfo> PutInfo(string endpoint)
        {
            return await _service.GetInfo(endpoint);
        }

        // <endpoint>/matches/<timestamp> GET
        [HttpGet("{endpoint}/matches/{timestamp}")]
        public async Task<MatcheResult> GetMatches(string endpoint, string timestamp)
        {
            return await _service.GetMatche(endpoint, timestamp);
        }

        // endpoint>/matches/<timestamp> PUT 
        [HttpPut("{endpoint}/matches/{timestamp}")]
        public async Task<MatcheResult> PutMatches(string endpoint, string timestamp)
        {
            return await _service.GetMatche(endpoint, timestamp);
        }

        // <endpoint>/stats GET
        [HttpPut("{endpoint}/stats")]
        public async Task<ServerState> GetState(string endpoint)
        {
            return await _service.GetState(endpoint);
        }

    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Services;
using GM.DAL.Entity;
using GM.DAL.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GM.Api.Controllers
{
    [Route("reports")]
    public class ReportController : Controller
    {
        private readonly IGenericRepository<Map> _gameModeRepository;
        private readonly IGenericRepository<Map> _mapRepository;
        private readonly IGenericRepository<Matche> _matcheRepository;
        private readonly IGenericRepository<Player> _playerRepository;
        private readonly IGenericRepository<Scoreboard> _scoreboardRepository;
        private readonly IGenericRepository<Server> _serverRepository;
        private readonly IReportService _service;

        public ReportController(IReportService service, IGenericRepository<Server> serverRepository,
            IGenericRepository<Scoreboard> scoreboardRepository, IGenericRepository<Player> playerRepository,
            IGenericRepository<Matche> matcheRepository, IGenericRepository<Map> mapRepository,
            IGenericRepository<Map> gameModeRepository)
        {
            _service = service;
            _serverRepository = serverRepository;
            _scoreboardRepository = scoreboardRepository;
            _playerRepository = playerRepository;
            _matcheRepository = matcheRepository;
            _mapRepository = mapRepository;
            _gameModeRepository = gameModeRepository;
        }

        // reports/recent-matches[/<count>] GET
        [HttpGet("recent-matches/{count}")]
        public async Task<IEnumerable<RecentMatche>> GetRecentMatches(int count)
        {
            return await _service.GetRecentMatches(count);
        }

        // reports/best-players[/<count>] GET
        [HttpGet("best-players/{count}")]
        public async Task<IEnumerable<BestPlayer>> GetBestPlayers(int count)
        {
            return await _service.GetBestPlayers(count);
        }

        // reports/best-players[/<count>] GET
        [HttpGet("popular-servers/{count}")]
        public async Task<IEnumerable<PopularServer>> GetPopularServers(int count)
        {
            return await _service.GetPopularServers(count);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Extension;
using GM.BLL.Infrastructure;
using GM.DAL.Entity;
using GM.DAL.Extension;
using GM.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace GM.BLL.Services
{
    public class ServerService : ServiceFactory<Server>, IServerService
    {
        public ServerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ServerInfo> GetInfo(string endpoint)
        {
            var query = await UnitOfWork.Context.Matches
                .Include("Server")
                .Include("GameMode")
                .OrderByDescending(a => a.StartTimeStamp)
                .GroupBy(a => a.ServerId)
                .Select(s => new
                {
                    server = s.FirstOrDefault().Server,
                    gameModes = s.Select(a => a.GameMode.Name)
                }).FirstOrDefaultAsync(a => a.server.GetServerEndpoint() == endpoint);
            var result = new ServerInfo
            {
                Name = query.server.Name,
                GameModes = query.gameModes.ToArray()
            };

            return result;
        }

        public async Task<IEnumerable<ServerInfo>> GetInfos()
        {
            var query = UnitOfWork.Context.Matches
                .Include("Server")
                .Include("GameMode")
                .OrderByDescending(a => a.StartTimeStamp)
                .GroupBy(a => a.ServerId)
                .Select(s => new
                {
                    server = s.FirstOrDefault().Server.Name,
                    gameModes = s.Select(a => a.GameMode.Name)
                });
            var results = await query.Select(s => new ServerInfo
            {
                Name = s.server,
                GameModes = s.gameModes.ToArray()
            }).ToArrayAsync();

            return results;
        }

        public async Task<MatcheResult> GetMatche(string endpoint, string timestamp)
        {
            var query = await UnitOfWork.Context.Matches
                .Include("Server")
                .Include("Map")
                .Include("GameMode")
                .Include("Scoreboards")
                .Include("Scoreboards.Player")
                .FirstOrDefaultAsync(a => a.Server.GetServerEndpoint() == endpoint && a.StartTimeStamp.GetDateTimestamp() == timestamp);
            var result = new MatcheResult
            {
                GameMode = query.GameMode.Name,
                Map = query.Map.Name,
                FragLimit = query.FragLimit,
                TimeLimit = query.TimeLimit,
                TimeElapsed = (DateTime.UtcNow - query.StartTimeStamp).TotalSeconds,
                Scoreboard = query.Scoreboards.OrderByDescending(a => a.Frags).ThenByDescending(a => a.Kills).ThenByDescending(a => a.Deaths).Select(a => new ScoreboardItem
                {
                    Name = a.Player.Name,
                    Kills = a.Kills,
                    Deaths = a.Deaths,
                    Frags = a.Frags
                }).ToArray()
            };

            return result;
        }

        public async Task<ServerState> GetState(string endpoint)
        {
            var query = await UnitOfWork.Context.Matches
                  .Include("Server")
                .Include("Map")
                .Include("GameMode")
                .Include("Scoreboards")
                .OrderByDescending(a => a.StartTimeStamp)
                .Where(w => w.Server.GetServerEndpoint() == endpoint)
                .ToArrayAsync();
            var result = new ServerState
            {
                TotalMatchesPlayed = query.Length,
                AverageMatchesPerDay = query.GroupBy(a => a.StartTimeStamp.Date).Select(s => s.Count()).Average(a => a),
                MaximumMatchesPerDay = query.GroupBy(a => a.StartTimeStamp.Date).Select(s => s.Count()).Max(a => a),
                AveragePopulation = query.GroupBy(a => a.Scoreboards.Count).Select(s => s.Count()).Average(a => a),
                MaximumPopulation = query.GroupBy(a => a.Scoreboards.Count).Select(s => s.Count()).Max(a => a),
                Top5Maps = query.OrderByDescending(a => a.Scoreboards.Count).Take(5).Select(s => s.Map.Name).ToArray(),
                Top5GameModes = query.OrderByDescending(a => a.Scoreboards.Count).Take(5).Select(s => s.GameMode.Name).ToArray(),
            };

            return result;
        }
    }
}
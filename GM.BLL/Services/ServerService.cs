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
                .Include(a => a.Server)
                .Include(a => a.GameMode)
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
                .Include(a => a.Server)
                .Include(a => a.GameMode)
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
                .Include(a => a.Server)
                .Include(a => a.Map)
                .Include(a => a.GameMode)
                .Include(a => a.Scoreboards)
                .FirstOrDefaultAsync(a => a.Server.GetServerEndpoint() == endpoint && a.StartTimeStamp.GetDateTimestamp() == timestamp);
            var results = new MatcheResult
            {
                GameMode = query.GameMode.Name,
                Map = query.Map.Name,
                FragLimit = query.FragLimit,
                TimeLimit = query.TimeLimit,
                TimeElapsed = (DateTime.UtcNow - query.StartTimeStamp).TotalSeconds,
                Scoreboard = query.Scoreboards.OrderByDescending(a => a.Frags).ThenByDescending(a => a.Kills).ThenByDescending(a => a.Deaths).Select(a => new ScoreboardItem
                {
                    Name = UnitOfWork.Context.Players.Find(a.PlayerId).Name,
                    Kills = a.Kills,
                    Deaths = a.Deaths,
                    Frags = a.Frags
                }).ToArray()
            };

            return results;
        }

        public Task<ServerState> GetState(string endpoint)
        {
            return Task.FromResult(new ServerState());
        }
    }
}
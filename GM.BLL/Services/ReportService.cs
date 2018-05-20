using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;
using GM.DAL.Extension;
using GM.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GM.BLL.Services
{
    public class ReportService : ServiceFactory, IReportService
    {
        public ReportService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<RecentMatche>> GetRecentMatches(int count)
        {
            var query = await UnitOfWork.Context.Matches
                .Include("Server")
                .Include("GameMode")
                .Include("Map")
                .Include("Scoreboards")
                .Include("Scoreboards.Player")
                .ToArrayAsync();
            var results = query
                .OrderByDescending(a => a.StartTimeStamp)
                .Take(count)
                .Select(s => new RecentMatche
                {
                    Server = s.Server.Name,
                    Timestamp = s.StartTimeStamp.GetDateTimestamp(),
                    Results = new MatcheResult
                    {
                        GameMode = s.GameMode.Name,
                        Map = s.Map.Name,
                        FragLimit = s.FragLimit,
                        TimeElapsed = (DateTime.UtcNow - s.StartTimeStamp).TotalSeconds,
                        TimeLimit = s.TimeLimit,
                        Scoreboard = s.Scoreboards.Select(a => new ScoreboardItem
                        {
                            Name = a.Player.Name,
                            Kills = a.Kills,
                            Deaths = a.Deaths,
                            Frags = a.Frags
                        }).ToArray()
                    }
                });

            return results;
        }

        public async Task<IEnumerable<BestPlayer>> GetBestPlayers(int count)
        {

            var query = await UnitOfWork.Context.Players
                .Include("Scoreboards")
                .Include("Scoreboards.Matche")
                .Include("Scoreboards.Matche.Server")
                .Include("Scoreboards.Matche.Map")
                .Include("Scoreboards.Matche.GameMode")
                .ToArrayAsync();
            var results = query.GroupBy(a => a.Name)
                .Select(a => new
                {
                    name = a.Key,
                    kills = a.Sum(b => b.Scoreboards.Sum(c => c.Kills)),
                    deaths = a.Sum(b => b.Scoreboards.Sum(c => c.Deaths))
                })
                .Select(s => new BestPlayer
                {
                    Name = s.name,
                    KillToDeathRatio = s.deaths > 0 ? (double)s.kills / s.deaths : 0
                })
                .OrderByDescending(a => a.KillToDeathRatio)
                .Take(count);

            return results;
        }

        public async Task<IEnumerable<PopularServer>> GetPopularServers(int count)
        {
            var query = await UnitOfWork.Context.Matches
                .Include("Server")
                .ToArrayAsync();
            var results = query.GroupBy(a => a.StartTimeStamp.Date)
                .Select(s => new
                {
                    endpoint = s.FirstOrDefault()?.Server?.GetServerEndpoint(),
                    name = s.FirstOrDefault()?.Server?.Name,
                    averageMatchesPerDay = s.Average(a => a.Scoreboards.Count)
                })
                .OrderByDescending(a => a.averageMatchesPerDay)
                .Take(count)
                .Select(s => new PopularServer
                {
                    Name = s.name,
                    AverageMatchesPerDay = s.averageMatchesPerDay,
                    Endpoint = s.endpoint
                });

            return results;
        }
    }
}
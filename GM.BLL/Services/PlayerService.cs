using System;
using System.Linq;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;
using GM.DAL.Entity;
using GM.DAL.Extension;
using GM.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GM.BLL.Services
{
    public class PlayerService : ServiceFactory<Player>, IPlayerService
    {
        public PlayerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<PlayerState> GetState(string name)
        {

            var query = await UnitOfWork.Context.Players
                .Where(w => w.Name == name)
                .Include("Scoreboards")
                .Include("Scoreboards.Matche")
                .Include("Scoreboards.Matche.Server")
                .Include("Scoreboards.Matche.Map")
                .Include("Scoreboards.Matche.GameMode")
                .FirstOrDefaultAsync();
            var result = new PlayerState
            {
                UniqueServers = query.Scoreboards.Select(s => s.Matche.ServerId).Distinct().Count(),
                FavoriteServer = query.Scoreboards.GroupBy(s => s.Matche.Server.Name).Select(a => new { name = a.Key, count = a.Count() }).OrderByDescending(a => a.count).FirstOrDefault()?.name,
                TotalMatchesWon = query.Scoreboards.Where(w => w.Matche.FragLimit == w.Frags).Sum(a => a.Kills),
                TotalMatchesPlayed = query.Scoreboards.Sum(a => a.Kills),
                LastMatchPlayed = query.Scoreboards.OrderByDescending(a => a.Matche.StartTimeStamp).FirstOrDefault()?.Matche?.StartTimeStamp,
                FavoriteGameMode = query.Scoreboards.GroupBy(s => s.Matche.GameMode.Name).Select(a => new { name = a.Key, count = a.Count() }).OrderByDescending(a => a.count).FirstOrDefault()?.name,
                MaximumMatchesPerDay = query.Scoreboards.GroupBy(a => a.Matche.StartTimeStamp.Date).Select(a => a.Count()).Max(a => a),
                AverageMatchesPerDay = query.Scoreboards.GroupBy(a => a.Matche.StartTimeStamp.Date).Select(a => a.Count()).Average(a => a),
                KillToDeathRatio = query.Scoreboards.Sum(a => a.Deaths) > 0 ? (double)query.Scoreboards.Sum(s => s.Kills) / query.Scoreboards.Sum(a => a.Deaths) : 0,
            };

            #region Average Scoreboard Percent

            /*
                    averageScoreboardPercent считается так:

                    Для конкретного матча scoreboardPercent = playersBelowCurrent / (totalPlayers - 1) * 100%​.

                    Пример 1, в таблице 4 игрока:

                    Player1 — 100%
                    Player2 — 66.666667%
                    Player3 — 33.333333%
                    Player4 — 0%

                    Пример 2, в таблице 3 игрока:

                    Player1 — 100%
                    Player2 — 50%
                    Player3 — 0%

                    Если в матче один игрок, scoreboardPercent = 100%.

                    averageScoreboardPercent — это средний scoreboardPercent данного игрока по всем сыгранным матчам.
             */

            #endregion

            return result;
        }
    }
}
﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;
using GM.DAL.Entity;

namespace GM.BLL.Services
{
    public interface IReportService : IServiceFactory
    {

        Task<IEnumerable<RecentMatche>> GetRecentMatches(int count);

        Task<IEnumerable<BestPlayer>> GetBestPlayers(int count);

        Task<IEnumerable<PopularServer>> GetPopularServers(int count);

    }
}

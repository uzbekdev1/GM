using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GM.BLL.Dto;
using GM.BLL.Infrastructure;
using GM.DAL.Entity;
using GM.DAL.Infrastructure;

namespace GM.BLL.Services
{
    public class ReportService : ServiceFactory, IReportService
    {

        public ReportService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IEnumerable<RecentMatche>> GetRecentMatches(int count)
        {
            return Task.FromResult<IEnumerable<RecentMatche>>(new RecentMatche[0]);
        }

        public Task<IEnumerable<BestPlayer>> GetBestPlayers(int count)
        {
            return Task.FromResult<IEnumerable<BestPlayer>>(new BestPlayer[0]);
        }

        public Task<IEnumerable<PopularServer>> GetPopularServers(int count)
        {
            return Task.FromResult<IEnumerable<PopularServer>>(new PopularServer[0]);
        }
    }
}
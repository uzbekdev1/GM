using System;

namespace GM.BLL.Dto
{
    public class RecentMatches
    {
        public string Server { get; set; }

        public DateTime Timestamp { get; set; }

        public MatcheResult Results { get; set; }

        public RecentMatches()
        {
            Results = new MatcheResult();
        }
    }
}
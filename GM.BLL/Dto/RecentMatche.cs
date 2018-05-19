using System;

namespace GM.BLL.Dto
{
    public class RecentMatche
    {
        public string Server { get; set; }

        public DateTime Timestamp { get; set; }

        public MatcheResult Results { get; set; }

        public RecentMatche()
        {
            Results = new MatcheResult();
        }
    }
}
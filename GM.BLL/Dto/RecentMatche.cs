using System;

namespace GM.BLL.Dto
{
    public class RecentMatche
    {
        public RecentMatche()
        {
            Results = new MatcheResult();
        }

        public string Server { get; set; }

        public string Timestamp { get; set; }

        public MatcheResult Results { get; set; }
    }
}
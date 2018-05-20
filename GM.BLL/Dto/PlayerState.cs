using System;

namespace GM.BLL.Dto
{
    public class PlayerState
    {
        public int TotalMatchesPlayed { get; set; }

        public int TotalMatchesWon { get; set; }

        public string FavoriteServer { get; set; }

        public int UniqueServers { get; set; }

        public string FavoriteGameMode { get; set; }

        public double AverageScoreboardPercent { get; set; }

        public int MaximumMatchesPerDay { get; set; }

        public double AverageMatchesPerDay { get; set; }

        public DateTime? LastMatchPlayed { get; set; }

        public double KillToDeathRatio { get; set; }
    }
}
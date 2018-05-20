namespace GM.BLL.Dto
{
    public class MatcheResult
    {
        public MatcheResult()
        {
            Scoreboard = new ScoreboardItem[0];
        }

        public string Map { get; set; }

        public string GameMode { get; set; }

        public int FragLimit { get; set; }

        public int TimeLimit { get; set; }

        public double TimeElapsed { get; set; }

        public ScoreboardItem[] Scoreboard { get; set; }
    }
}
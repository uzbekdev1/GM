namespace GM.BLL.Dto
{
    public class ServerState
    {

        public int TotalMatchesPlayed { get; set; }

        public int MaximumMatchesPerDay { get; set; }

        public float AverageMatchesPerDay { get; set; }

        public int MaximumPopulation { get; set; }

        public float AveragePopulation { get; set; }

        public string[] Top5GameModes { get; set; }

        public string[] Top5Maps { get; set; }

        public ServerState()
        {
            Top5Maps = new string[0];
            Top5GameModes = new string[0];
        }

    }
}
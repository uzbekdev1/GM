namespace GM.BLL.Dto
{
    public class ServerInfo
    {
        public ServerInfo()
        {
            GameModes = new string[0];
        }

        public string Name { get; set; }

        public string[] GameModes { get; set; }
    }
}
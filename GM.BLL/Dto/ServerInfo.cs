namespace GM.BLL.Dto
{
    public class ServerInfo
    {

        public string Name { get; set; }

        public string[] GameModes { get; set; }

        public ServerInfo()
        {
            GameModes = new string[0];
        }

    }
}

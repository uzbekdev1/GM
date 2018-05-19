namespace GM.BLL.Dto
{
    public class ServerList
    {
        public string Endpoint { get; set; }

        public ServerInfo ServerInfo { get; set; }

        public ServerList()
        {
            ServerInfo = new ServerInfo();
        }

    }
}

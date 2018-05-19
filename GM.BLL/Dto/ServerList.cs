namespace GM.BLL.Dto
{
    public class ServerList
    {
        public ServerList()
        {
            ServerInfo = new ServerInfo();
        }

        public string Endpoint { get; set; }

        public ServerInfo ServerInfo { get; set; }
    }
}
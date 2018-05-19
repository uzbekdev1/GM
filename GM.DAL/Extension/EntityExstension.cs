using GM.DAL.Entity;

namespace GM.DAL.Extension
{
    public static class EntityExstension
    {
        public static string GetServerEndpoint(this Server server)
        {
            return $"{server.Hostname}-{server.Port}";
        }
    }
}
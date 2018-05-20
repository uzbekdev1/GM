using System;
using GM.DAL.Entity;

namespace GM.DAL.Extension
{
    public static class EntityExstension
    {
        public static string GetServerEndpoint(this Server server)
        {
            if (server == null)
                return String.Empty;

            return $"{server.Hostname}-{server.Port}";
        }

        public static string GetDateTimestamp(this DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
        }

    }
}
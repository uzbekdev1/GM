using Microsoft.EntityFrameworkCore;

namespace GM.DAL.Extension
{
    public static class DbExtensions
    {
        public static void SafeConfigure(this DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString, a => a.MigrationsAssembly("GM.DAL").CommandTimeout(3600))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(false);
        }
    }
}
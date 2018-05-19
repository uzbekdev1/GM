using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GM.DAL;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GM.Api
{
    public class Program
    {
        private static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var db = services.GetRequiredService<ApplicationDbContext>();

                    //enable auto migration
                    if (db.Database.EnsureCreated())
                    {
                        logger.LogInformation("Database created successfully.");
                    }
                    else
                    {
                        db.Database.Migrate();

                        logger.LogInformation("Database migrated successfully.");
                    }

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred.");
                }
            }

            host.Run();
        }

    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using GM.Api;
using GM.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GM.App
{
    internal sealed class StatServer : IDisposable
    {
        private readonly IWebHost _webHost;

        public StatServer(string prefix)
        {
            _webHost = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(prefix)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }

        public void Dispose()
        {
            _webHost.StopAsync().GetAwaiter().GetResult();
            _webHost.Dispose();

            GC.SuppressFinalize(this);
        }

        public void Run()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var db = services.GetRequiredService<ApplicationDbContext>();

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

            _webHost.Run();
        }
    }
}
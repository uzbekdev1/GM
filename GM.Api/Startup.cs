using GM.BLL.Services;
using GM.Common;
using GM.DAL;
using GM.DAL.Entity;
using GM.DAL.Extension;
using GM.DAL.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace GM.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //mvc            
            services.AddMvc(options =>
            {
                //TODO:
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                options.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
                options.SerializerSettings.FloatParseHandling = FloatParseHandling.Double;
                options.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    CamelCaseText = true,
                    AllowIntegerValues = true
                });
            });

            //logs
            services.AddLogging();

            //http
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //db
            services.AddDbContext<ApplicationDbContext>(options => options.SafeConfigure(AppSettings.ConnectionString));

            //unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //repos
            services.AddScoped<IGenericRepository<Server>, GenericRepository<Server>>();
            services.AddScoped<IGenericRepository<GameMode>, GenericRepository<GameMode>>();
            services.AddScoped<IGenericRepository<Map>, GenericRepository<Map>>();
            services.AddScoped<IGenericRepository<Player>, GenericRepository<Player>>();
            services.AddScoped<IGenericRepository<Scoreboard>, GenericRepository<Scoreboard>>();
            services.AddScoped<IGenericRepository<Matche>, GenericRepository<Matche>>();

            //services
            services.AddScoped<IServerService, ServerService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IReportService, ReportService>();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Многопользовательской игры-шутера RESTful API",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Email = "elyor.blog@gmail.com",
                        Name = "Elyor Latipov",
                        Url = "https://www.linkedin.com/in/levdeo/"
                    },
                    Description =
                        @"Игровые сервера анонсируют себя advertise-запросами, затем присылают результаты каждого завершенного матча. Сервер статистики аккумулирует разную статистику по результатам матчей и отдает её по запросам (статистика по серверу, статистика по игроку, топ игроков и т.д.)."
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if DEBUG
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseDatabaseErrorPage();
#endif

            app.UseMvc();
            app.UseMvcWithDefaultRoute();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowedToAllowWildcardSubdomains());

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Многопользовательской игры-шутера";
                c.RoutePrefix = string.Empty;
                c.HeadContent = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Многопользовательской игры-шутера RESTful API V1");
            });
        }
    }
}
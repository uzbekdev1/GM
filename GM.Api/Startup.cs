using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GM.DAL;
using GM.DAL.Entity;
using GM.DAL.Extension;
using GM.DAL.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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
            }).AddJsonOptions(options =>
                {
                    options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
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

            //db
            services.AddDbContext<ApplicationDbContext>(options => options.SafeConfigure(Configuration.GetConnectionString("DefaultConnection")));

            //unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //repos
            services.AddScoped<IGenericRepository<Server>, GenericRepository<Server>>();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Игровые сервер RESTful API",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Email = "elyor.blog@gmail.com",
                        Name = "Elyor Latipov",
                        Url = "https://www.linkedin.com/in/levdeo/"
                    },
                    Description = @"Игровые сервера анонсируют себя advertise-запросами, затем присылают результаты каждого завершенного матча. Сервер статистики аккумулирует разную статистику по результатам матчей и отдает её по запросам (статистика по серверу, статистика по игроку, топ игроков и т.д.)."
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseMvc();
            app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Игровые сервер";
                c.RoutePrefix = string.Empty;
                c.HeadContent = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Игровые сервер RESTful API V1");
            });

        }
    }
}

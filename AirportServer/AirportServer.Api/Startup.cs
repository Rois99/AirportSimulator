using AirportServer.Api.Hubs;
using AirportServer.Domain.Interfaces;
using AirportServer.Infra;
using AirportServer.Infra.Repositories;
using AirportServer.Services.Interfaces;
using AirportServer.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AirportServer.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; set; }


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string conncetionString = _configuration.GetConnectionString("Default");
            services.AddDbContext<AirportContext>(options => options.UseSqlServer(conncetionString), ServiceLifetime.Singleton);

            services.AddControllers();
            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSignalR();

            services.AddSingleton<IAirportLogic, AirportLogicService>();
            services.AddSingleton<IAirportRepository, AirportRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AirportContext airportContext)
        {
            airportContext.Database.EnsureDeleted();
            airportContext.Database.EnsureCreated();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            airportContext.Database.EnsureDeleted();
            airportContext.Database.EnsureCreated();

            app.UseRouting();

            app.UseCors(builder => builder
                .WithOrigins(new[] { "http://localhost:3000" })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                );


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<AirportHub>("/airport");
            });
        }
    }
}

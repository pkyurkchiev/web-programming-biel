using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PM.WebServices.Commands.Handlers;
using PM.WebServices.Models;
using PM.WebServices.Queries.Handlers;
using PM.WebServices.Services;
using AuthenticationService = PM.WebServices.Services.AuthenticationService;

namespace PM.WebServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parking Web Services", Version = "v1" });
            });

            services.AddDbContextPool<DbContext, ParkingContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                );
            });

            services.AddScoped<AuthenticationService>();

            services.AddScoped<ParkingCommandHandler>();
            services.AddScoped<ParkingQueryHandler>();

            services.AddScoped<CommandStoreService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

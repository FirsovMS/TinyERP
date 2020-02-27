using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TinyErpWebService.Models;
using TinyErpWebService.Models.DTO;

namespace TinyErpWebService
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
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddTransient<AppSettings>(x => new AppSettings
            {
                EnableDevelopmentExceptions = Configuration.GetValue<bool>("EnableDevelopmentExceptions")
            });
            
            AddDbContext(services);

            services.AddIdentity<Employee, IdentityRole>()
                .AddEntityFrameworkStores<EmployeeContext>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });
        }

        private void AddDbContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TinyErpContext");
            services.AddDbContextPool<ItemsContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddDbContextPool<EmployeeContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, AppSettings appSettings)
        {
            if (appSettings.EnableDevelopmentExceptions)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "api/{controller=Item}/{action=Get}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });
        }
    }
}

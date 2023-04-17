using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UpWork.Database;

namespace UpWork.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            //TODO
        }

        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(conn);
            });
        }

        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
        }

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}

namespace UpWork.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {

        }

        public static void AddCustomDbContext(this IServiceCollection services)
        {

        }

        public static void AddCustomServices(this IServiceCollection services)
        {

        }

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}

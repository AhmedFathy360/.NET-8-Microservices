namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices
            ( this IServiceCollection services, IConfiguration configuration)
        {
            // Register API services here
            // Example: services.AddControllers();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure API middleware here
            // Example: app.UseRouting(); app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            return app;
        }
    }
}

using CustomerIdentityService.Infrastructure.DependencyInjection;

namespace CustomerIdentityService.API.DependencyInjection
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            return services;
        }
    }
}

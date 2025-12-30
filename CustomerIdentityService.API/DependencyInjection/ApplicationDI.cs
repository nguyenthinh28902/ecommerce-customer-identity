using CustomerIdentityService.Application.DependencyInjection;

namespace CustomerIdentityService.API.DependencyInjection
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplicationDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationServices(configuration);
            return services;
        }
    }
}

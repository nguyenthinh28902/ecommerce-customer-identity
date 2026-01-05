using CustomerIdentityService.Application.DependencyInjection;
using CustomerIdentityService.Core.Abstractions.Persistence;
using CustomerIdentityService.Infrastructure.Repositories;

namespace CustomerIdentityService.API.DependencyInjection
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplicationDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationServices(configuration);
            //automapper
            services.AddAutoMapperServiceRegistration(configuration);
            return services;
        }
    }
}

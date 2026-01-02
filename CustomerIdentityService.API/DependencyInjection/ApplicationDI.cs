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
            //add kiến trúc repo and UoW
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

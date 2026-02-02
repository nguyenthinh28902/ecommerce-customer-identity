using CustomerIdentityService.Application.Services.Authentication;
using CustomerIdentityService.Application.Services.CustomerServices;
using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Interfaces.Services;
using CustomerIdentityService.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerIdentityService.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICustomerservice, Customerservice>();


            return services;
        }
    }
}

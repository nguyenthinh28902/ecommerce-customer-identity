using CustomerIdentityService.Application.Services.Authentication;
using CustomerIdentityService.Application.Services.CustomerServices;
using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Interfaces.Security;
using CustomerIdentityService.Core.Interfaces.Services;
using CustomerIdentityService.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICustomerservice, Customerservice>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}

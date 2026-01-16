using CustomerIdentityService.Application.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.API.Configurations
{
    public static class AuthenticationIdentityServer
    {
        public static IServiceCollection AddAuthenticationIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
           
            return services;
        }
    }
}

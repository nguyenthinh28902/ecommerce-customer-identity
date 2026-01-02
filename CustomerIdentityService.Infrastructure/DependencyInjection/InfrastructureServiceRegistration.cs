using CustomerIdentityService.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var CustomerAppLocalstring = configuration.GetConnectionString("CustomerAppLocal") ?? string.Empty;
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseOracle(CustomerAppLocalstring, oracleOptions => { oracleOptions.CommandTimeout(60); }));



            return services;
        }
    }
}

using CustomerIdentityService.Core.Abstractions.Persistence;
using CustomerIdentityService.Core.Models.Settings;
using CustomerIdentityService.Infrastructure.Persistence.DbContexts;
using CustomerIdentityService.Infrastructure.Repositories;
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
            ConnectionStrings.CustomerAppLocal = configuration.GetConnectionString("CustomerAppLocal") ?? string.Empty;
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseOracle(ConnectionStrings.CustomerAppLocal, oracleOptions => { oracleOptions.CommandTimeout(60); }));
            //add kiến trúc repo and UoW
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}

using CustomerIdentityService.Core.Enums;
using CustomerIdentityService.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;

namespace CustomerIdentityService.API.Configurations
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(
              configuration.GetSection("JwtSettings"));
            services.Configure<GoogleWebApiSettings>(
              configuration.GetSection("web"));
            services.Configure<ExternalProviderOptions>(
               configuration.GetSection("ExternalProviders"));

            return services;
        }
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {


            //tạo cấu hình
            var jwtSettings = configuration
                .GetSection("JwtSettings")
                .Get<JwtSettings>()
                ?? throw new InvalidOperationException("JwtSettings missing");
            var googleWebApiSettings = configuration
              .GetSection("web")
              .Get<GoogleWebApiSettings>()
              ?? throw new InvalidOperationException("JwtSettings missing");
            
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7123";
                    options.Audience = "customer.internal";
                });


            services.AddAuthorization();
            return services;
        }
    }
}

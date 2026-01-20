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
                    options.Authority = configuration["EcommerceApiGateWay:BaseUrl"];
                    options.Audience = "customer.internal";
                    // Chấp nhận chứng chỉ tự ký từ gateway-service khi kiểm tra token nội bộ
                    options.BackchannelHttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };
                });


            services.AddAuthorization();
            return services;
        }
    }
}

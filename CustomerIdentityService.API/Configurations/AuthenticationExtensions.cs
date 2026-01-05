using CustomerIdentityService.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey!)
                    )
                };
            });

            services.AddAuthorization();

           


            return services;
        }
    }
}

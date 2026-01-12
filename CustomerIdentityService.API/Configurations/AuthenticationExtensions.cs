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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = "Cookies";
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
            })
            .AddCookie("Cookies")
            .AddGoogle(ProviderName.Google.ToString(), options =>
            {
                // Lấy thông tin từ file appsettings.json
                options.ClientId = googleWebApiSettings.client_id;
                options.ClientSecret = googleWebApiSettings.client_secret;
                options.Scope.Add("profile");
                // Quan trọng: Lưu lại tokens để sau này có thể lấy AccessToken nếu cần
                options.SaveTokens = true;
                //options.ClaimActions.MapJsonKey("sub", "id"); // Map trường 'id' từ Google thành 'sub'
                // Dòng quan trọng nhất:
                options.ClaimActions.MapJsonKey("image_url", "picture");
                //options.Events.OnTicketReceived = ctx =>
                //{
                //    var identity = ctx.Principal.Identity as ClaimsIdentity;
                //    if (identity != null)
                //    {
                //        // Kiểm tra nếu chưa có claim 'sub', lấy giá trị từ NameIdentifier đắp qua
                //        if (!identity.HasClaim(c => c.Type == "sub"))
                //        {
                //            var nameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier);
                //            if (nameIdentifier != null)
                //            {
                //                identity.AddClaim(new Claim("sub", nameIdentifier.Value));
                //            }
                //        }
                //    }
                //    return Task.CompletedTask;
                //};
            });

            services.AddAuthorization();
            return services;
        }
    }
}

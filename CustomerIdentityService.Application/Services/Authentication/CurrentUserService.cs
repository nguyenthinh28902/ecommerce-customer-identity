using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace CustomerIdentityService.Application.Services.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;

                // Kiểm tra cả 'sub' (chuẩn JWT) và 'NameIdentifier' (chuẩn .NET)
                var value = user?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                            ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return int.TryParse(value, out var userId) ? userId : 0;
            }
        }

        public string? Email
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                // Kiểm tra cả 'sub' (chuẩn JWT) và 'NameIdentifier' (chuẩn .NET)
                var value = user?.FindFirst(JwtRegisteredClaimNames.Email)?.Value
                            ?? user?.FindFirst(ClaimTypes.Email)?.Value;
                return value ?? string.Empty;
            }
        }
    }
}

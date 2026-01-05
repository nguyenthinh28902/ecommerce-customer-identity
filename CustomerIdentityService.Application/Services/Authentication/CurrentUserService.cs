using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using Microsoft.AspNetCore.Http;
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
                var value = _httpContextAccessor.HttpContext?.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return int.TryParse(value, out var userId) ? userId : 0;
            }
        }

        public string? Email =>
            _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.Email)?.Value;
    }
}

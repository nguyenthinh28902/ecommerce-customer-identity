using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Dtos.Customers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerIdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("google-login")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleChallenge([FromQuery] string returnUrl = "/", string provider = "")
        {
            var redirectUrl = Url.Action("GoogleResponse", "Auth", new { returnUrl });

            // Yêu cầu ASP.NET Challenge tới Google
            var properties = _authService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        {
            var jwt = await _authService.GoogleResponse();
            if (!string.IsNullOrEmpty(jwt))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true, // Ngăn không cho truy cập cookie từ JavaScript
                    Secure = true, // Chỉ gửi cookie qua HTTPS
                    SameSite = SameSiteMode.Strict, // Ngăn chặn CSRF
                    Expires = DateTime.UtcNow.AddHours(5) // Thời gian tồn tại của cookie
                };
                Response.Cookies.Append("Authentication", jwt, cookieOptions);
            }
            else
            {
                returnUrl = Url.Action("Error", "Login");
            }
            return Redirect(returnUrl);
        }

    }
}

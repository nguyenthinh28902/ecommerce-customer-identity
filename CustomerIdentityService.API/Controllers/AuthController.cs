using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Dtos.Customers;
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

        [HttpPost("google-login")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromBody] string idToken)
        {
            var token = await _authService.AuthenticateGoogleUser(idToken);
            return Ok(new { AccessToken = token });
        }

       
    }
}

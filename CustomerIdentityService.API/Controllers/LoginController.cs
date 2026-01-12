using CustomerIdentityService.Core.Models;
using CustomerIdentityService.Core.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CustomerIdentityService.API.Controllers
{
    public class LoginController : Controller
    {
        private readonly ExternalProviderOptions _externalOptions;
        public LoginController(IOptions<ExternalProviderOptions> options) {
            _externalOptions = options.Value;
        }
        public IActionResult Index(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                // Sử dụng danh sách string từ cấu hình
                AllowedProviders = _externalOptions.Schemes
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}

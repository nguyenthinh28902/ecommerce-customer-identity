using AutoMapper;
using CustomerIdentityService.Core;
using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Abstractions.Persistence;
using CustomerIdentityService.Core.Dtos.Google;
using CustomerIdentityService.Core.Enums;
using CustomerIdentityService.Core.Interfaces.Security;
using CustomerIdentityService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerservice _customerservice;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IGoogleAuthService googleAuthService, IUnitOfWork unitOfWork, ICustomerservice customerservice, IMapper mapper,
            IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _googleAuthService = googleAuthService;
            _unitOfWork = unitOfWork;
            _customerservice = customerservice;
            _mapper = mapper;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AuthenticateGoogleUser(string idToken)
        {
            // 1. Kiểm tra Token với Google
            var googleUser = await _googleAuthService.VerifyGoogleTokenAsync(idToken);
            if (googleUser == null) throw new Exception("Xác thực Google thất bại");
            // 2. Tìm User trong DB Oracle (bảng CUSTOMER_AUTH_PROVIDERS)
            var provider = await _unitOfWork.Repository<CustomerAuthProvider>().FirstOrDefaultAsync(p => p.Provider == ProviderName.Google.ToString()
            && p.ProviderUserId == googleUser.ProviderUserId);

            if (provider == null)
            {
                // 3. Nếu chưa có: Tạo mới Customer và tạo liên kết Provider
                var newCustomer = _mapper.Map<Customer>(googleUser);
                var newCustomerAuthProvider = new CustomerAuthProvider();
                newCustomerAuthProvider.Provider = ProviderName.Google.ToString();
                newCustomerAuthProvider.ProviderUserId = googleUser.ProviderUserId;
                newCustomerAuthProvider.CreatedAt = DateTime.Now;
                newCustomer.CustomerAuthProviders.Add(newCustomerAuthProvider);

                var result = await _customerservice.Registration(newCustomer);
                if (result.IsSuccess == false) { throw new Exception("Xác thực Google thất bại"); }
                provider = result.Data?.CustomerAuthProviders.FirstOrDefault();
            }
            return _jwtService.GenerateToken(provider.CustomerId, googleUser.Email);
        }
        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            // Tạo đối tượng properties của ASP.NET Core
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

            // Bạn có thể thêm các tùy chỉnh khác nếu muốn, ví dụ:
            // properties.Items.Add("login_hint", "user@example.com"); 

            return properties;
        }
    
        public async Task<string> GoogleResponse()
        {
            var resultHttpContext = await _httpContextAccessor.HttpContext.AuthenticateAsync("Cookies");
            var user =  _httpContextAccessor.HttpContext.User;
            if (!resultHttpContext.Succeeded)
            {
                return string.Empty;
            }
            var googleUser = new UserInfoSigninDto();
            googleUser.ProviderUserId = resultHttpContext.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            googleUser.Email = resultHttpContext.Principal.FindFirst(ClaimTypes.Email)?.Value;
            googleUser.Name = resultHttpContext.Principal.FindFirst(ClaimTypes.Name)?.Value;
            googleUser.Picture = resultHttpContext.Principal.FindFirst("image_url")?.Value; // Google avatar
            var result = await _customerservice.CreateCustomerSingin(googleUser, ProviderName.Google.ToString());
            if (!result.IsSuccess) { 
                return string.Empty;
            }
            var provider = result.Data;
            return _jwtService.GenerateToken(provider.CustomerId, googleUser.Email);
        }
    }
}
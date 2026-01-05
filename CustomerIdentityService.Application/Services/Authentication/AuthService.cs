using AutoMapper;
using CustomerIdentityService.Core;
using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Abstractions.Persistence;
using CustomerIdentityService.Core.Interfaces.Security;
using CustomerIdentityService.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public AuthService(IGoogleAuthService googleAuthService, IUnitOfWork unitOfWork, ICustomerservice customerservice, IMapper mapper,
            IJwtService jwtService)
        {
            _googleAuthService = googleAuthService;
            _unitOfWork = unitOfWork;
            _customerservice = customerservice;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<string> AuthenticateGoogleUser(string idToken)
        {
            // 1. Kiểm tra Token với Google
            var googleUser = await _googleAuthService.VerifyGoogleTokenAsync(idToken);
            if (googleUser == null) throw new Exception("Xác thực Google thất bại");
            // 2. Tìm User trong DB Oracle (bảng CUSTOMER_AUTH_PROVIDERS)
            var provider = await _unitOfWork.Repository<CustomerAuthProvider>().FirstOrDefaultAsync(p => p.Provider == "GOOGLE" 
            && p.ProviderUserId == googleUser.ProviderUserId);

            if (provider == null)
            {
                // 3. Nếu chưa có: Tạo mới Customer và tạo liên kết Provider
                var newCustomer = _mapper.Map<Customer>(googleUser);
                var newCustomerAuthProvider = new CustomerAuthProvider();
                newCustomerAuthProvider.Provider = "GOOGLE";
                newCustomerAuthProvider.ProviderUserId = googleUser.ProviderUserId;
                newCustomerAuthProvider.CreatedAt = DateTime.Now;
                newCustomer.CustomerAuthProviders.Add(newCustomerAuthProvider);

                var result = await _customerservice.Registration(newCustomer);
                if (result.IsSuccess == false) { throw new Exception("Xác thực Google thất bại"); }
                provider = result.Data?.CustomerAuthProviders.FirstOrDefault();
            }
            return _jwtService.GenerateToken(provider.CustomerId, googleUser.Email);
        }
    }
}
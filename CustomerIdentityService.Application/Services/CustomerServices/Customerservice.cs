using AutoMapper;
using CustomerIdentityService.Core;
using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Abstractions.Persistence;
using CustomerIdentityService.Core.Dtos.Customers;
using CustomerIdentityService.Core.Dtos.Google;
using CustomerIdentityService.Core.Interfaces.Services;
using CustomerIdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Application.Services.CustomerServices
{
    public class Customerservice : ICustomerservice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public Customerservice(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

       

        public async Task<Result<Customer>> Registration(Customer customer)
        {
            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(); // Để lấy ID
            if (customer.Id != null && customer.Id != 0)
            {
                return Result<Customer>.Failure("Đăng ký thất bại");
            }
            return Result<Customer>.Success(customer, "Đăng ký thành công");
        }

        public async Task<Result<CustomerDto>> GetAuthenticatedCustomer()
        {
            var userId = _currentUserService.UserId;

            var customer = await _unitOfWork.Repository<Customer>().FindAsync(userId);
            if (customer == null) return Result<CustomerDto>.Failure("Thông tin khách hàng không tồn tại");
            var newCustomerDto = _mapper.Map<CustomerDto>(customer);

            return Result<CustomerDto>.Success(newCustomerDto, "Thông tin khách hàng");
        }
        public async Task<Result<CustomerAuthProvider>> CreateCustomerSingin(UserInfoSigninDto googleUser, string ProviderName)
        {
            var provider = await _unitOfWork.Repository<CustomerAuthProvider>().FirstOrDefaultAsync(p => p.Provider == ProviderName
             && p.ProviderUserId == googleUser.ProviderUserId);

            if (provider == null)
            {
                // 3. Nếu chưa có: Tạo mới Customer và tạo liên kết Provider
                var newCustomer = _mapper.Map<Customer>(googleUser);
                var newCustomerAuthProvider = new CustomerAuthProvider();
                newCustomerAuthProvider.Provider = ProviderName;
                newCustomerAuthProvider.ProviderUserId = googleUser.ProviderUserId;
                newCustomerAuthProvider.CreatedAt = DateTime.Now;
                newCustomer.CustomerAuthProviders.Add(newCustomerAuthProvider);

                var result = await Registration(newCustomer);
                if (result.IsSuccess == false) { throw new Exception("Xác thực Google thất bại"); }
                provider = result.Data?.CustomerAuthProviders.FirstOrDefault();
            }
            return Result<CustomerAuthProvider>.Success(provider, "Đăng ký thành công");
        }

    }
}

using AutoMapper;
using CustomerIdentityService.Core;
using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using CustomerIdentityService.Core.Abstractions.Persistence;
using CustomerIdentityService.Core.Dtos.Customers;
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
        public Customerservice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}

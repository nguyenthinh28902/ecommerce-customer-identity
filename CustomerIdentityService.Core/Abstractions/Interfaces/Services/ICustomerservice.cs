using CustomerIdentityService.Core;
using CustomerIdentityService.Core.Dtos.Customers;
using CustomerIdentityService.Core.Dtos.Google;
using CustomerIdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Interfaces.Services
{
    public interface ICustomerservice
    {
       Task<Result<Customer>> Registration(Customer customer);
        Task<Result<CustomerDto>> GetAuthenticatedCustomer();
        Task<Result<CustomerAuthProvider>> CreateCustomerSingin(UserInfoSinginDto googleUser, string ProviderName);
    }
}

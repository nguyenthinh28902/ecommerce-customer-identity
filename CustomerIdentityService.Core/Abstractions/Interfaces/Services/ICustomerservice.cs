using CustomerIdentityService.Core.Dtos.Customers;
using CustomerIdentityService.Core.Dtos.Google;
using CustomerIdentityService.Core.Entities;
using CustomerIdentityService.Core.Models;

namespace CustomerIdentityService.Core.Interfaces.Services
{
    public interface ICustomerservice
    {
        Task<Result<Customer>> Registration(Customer customer);
        Task<Result<CustomerDto>> GetAuthenticatedCustomer();
        Task<Result<ConfirmCustomerInforResponse>> CreateCustomerSingin(UserInfoSigninDto googleUser, string ProviderName);
    }
}

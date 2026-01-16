using CustomerIdentityService.Core.Dtos.Customers;
using CustomerIdentityService.Core.Dtos.Google;
using CustomerIdentityService.Core.Enums;
using CustomerIdentityService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerIdentityService.API.Controllers
{
    [Route("api/khach-hang")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerservice _customerservice;
        public CustomerController(ICustomerservice customerservice) { 
            _customerservice = customerservice;
        }

        [HttpPost("xac-nhan-thong-tin-khach-hang")]
        [Authorize]
        public async Task<IActionResult> ConfirmCustomerInformation([FromBody] ConfirmCustomerDto userInfoSigninDto)
        {
            var result = await _customerservice.CreateCustomerSingin(userInfoSigninDto.Request, userInfoSigninDto.ProviderName);
            var createCustomerResponse = new CreateCustomerResponse();
            if (!result.IsSuccess)
            {
                return Ok(null);
            }
            var provider = result.Data;
            createCustomerResponse.CustomerId = provider.CustomerId;
            return Ok(createCustomerResponse);
        }


        [HttpGet("thong-tin-chi-tiet")]
        [Authorize]
        public async Task<IActionResult> GetCurrentCustomerInfo()
        {
            var data = await _customerservice.GetAuthenticatedCustomer();
            return Ok(data);
        }
    }
}

using CustomerIdentityService.Core.Dtos.Customers;
using CustomerIdentityService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CustomerIdentityService.API.Controllers
{
    [Route("api/khach-hang")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerservice _customerservice;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerservice customerservice
            , ILogger<CustomerController> logger)
        {
            _customerservice = customerservice;
            _logger = logger;
        }

        [HttpPost("xac-nhan-thong-tin-khach-hang")]
        [Authorize]
        public async Task<IActionResult> ConfirmCustomerInformation([FromBody] ConfirmCustomerDto userInfoSigninDto)
        {
            var result = await _customerservice.CreateCustomerSingin(userInfoSigninDto.Request, userInfoSigninDto.ProviderName);
            _logger.LogInformation($"data check: {JsonSerializer.Serialize(result)}");
            if (!result.IsSuccess)
            {
                return Ok(null);
            }
            return Ok(result);
        }


        [HttpGet("thong-tin")]
        public async Task<IActionResult> GetCurrentCustomerInfo()
        {
            var data = await _customerservice.GetAuthenticatedCustomer();
            return Ok(data);
        }
    }
}

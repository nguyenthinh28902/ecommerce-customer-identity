using CustomerIdentityService.Core.Dtos.Customers;
using CustomerIdentityService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerIdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerservice _customerservice;
        public CustomerController(ICustomerservice customerservice) { 
            _customerservice = customerservice;
        }

        [HttpGet("thong-tin-chi-tiet-khach-hang")]
        [Authorize]
        public async Task<IActionResult> GetCurrentCustomerInfo()
        {
            var data = await _customerservice.GetAuthenticatedCustomer();
            return Ok(data);
        }
    }
}

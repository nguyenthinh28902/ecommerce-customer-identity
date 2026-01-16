using CustomerIdentityService.Core.Dtos.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Dtos.Customers
{
    public class ConfirmCustomerDto
    {
        public UserInfoSigninDto Request { get; set; }
        public string ProviderName { get; set; }
    }
}

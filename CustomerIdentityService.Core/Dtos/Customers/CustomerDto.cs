using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Dtos.Customers
{
    public class CustomerDto
    {
        // System username (generate or map from email)
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set;}
    }
}

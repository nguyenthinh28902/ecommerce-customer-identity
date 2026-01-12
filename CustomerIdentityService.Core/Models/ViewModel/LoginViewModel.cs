using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Models.ViewModel
{
    public class LoginViewModel
    {
        // Chứa đường dẫn quay lại sau khi login thành công
        public string ReturnUrl { get; set; }

        // Bạn có thể thêm các field khác nếu cần
        public List<string> AllowedProviders { get; set; } = new List<string>();
    }
}

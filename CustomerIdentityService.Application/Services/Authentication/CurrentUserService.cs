using CustomerIdentityService.Core.Abstractions.Interfaces.Security;
using Microsoft.AspNetCore.Http;

namespace CustomerIdentityService.Application.Services.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int CustomerId // Sửa lại đúng chính tả từ CusomerId thành CustomerId
        {
            get
            {
                // 1. Ưu tiên lấy từ Header do Gateway gán vào
                var headerId = _httpContextAccessor.HttpContext?.Request.Headers["X-User-Id"].ToString();
                if (int.TryParse(headerId, out int id)) return id;



                return 0; // Trả về 0 nếu là khách vãng lai
            }
        }

        public string? Email
        {
            get
            {
                // 1. Lấy từ Header Gateway
                var email = _httpContextAccessor.HttpContext?.Request.Headers["X-User-Email"].ToString();
                return email;
            }
        }

        public string? PhoneNumber
        {
            get
            {
                // 1. Lấy từ Header Gateway
                var phone = _httpContextAccessor.HttpContext?.Request.Headers["X-User-Phone"].ToString();
                return phone;
            }
        }

        // Bổ sung thêm thuộc tính kiểm tra đăng nhập nhanh
        public bool IsAuthenticated => CustomerId > 0;
    }
}
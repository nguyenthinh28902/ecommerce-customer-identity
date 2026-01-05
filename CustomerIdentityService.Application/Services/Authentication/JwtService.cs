using CustomerIdentityService.Core.Interfaces.Security;
using CustomerIdentityService.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Application.Services.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        public JwtService(IOptions<JwtSettings> jwtSettings) { 
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(int customerId, string email)
        {
            // 1. Tạo danh sách các Claim (Thông tin nhúng vào token)
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, customerId.ToString()), // ID người dùng
            new Claim(JwtRegisteredClaimNames.Email, email),               // Email
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Mã định danh token duy nhất
            };
            // 2. Cấu hình chìa khóa ký
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 2. Thiết lập thời hạn và thông tin người cấp
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

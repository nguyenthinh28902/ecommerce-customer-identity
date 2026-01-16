using CustomerIdentityService.Core.Dtos.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Interfaces.Security
{
    public interface IGoogleAuthService
    {
        Task<UserInfoSigninDto?> VerifyGoogleTokenAsync(string idToken);
    }
}

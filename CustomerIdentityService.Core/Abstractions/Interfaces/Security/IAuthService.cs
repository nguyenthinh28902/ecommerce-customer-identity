using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Abstractions.Interfaces.Security
{
    public interface IAuthService
    {
        Task<string> AuthenticateGoogleUser(string idToken);
    }
}

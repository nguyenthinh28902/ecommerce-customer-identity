using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Dtos.Google
{
   public record GoogleUserInfoDto(string Email, string Name, string Picture, string ProviderUserId);
}

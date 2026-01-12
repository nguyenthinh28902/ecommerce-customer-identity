using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Dtos.Google
{
    public class UserInfoSinginDto
    {

        public string ProviderUserId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public UserInfoSinginDto() { }
        public UserInfoSinginDto(string Email, string Name, string Picture, string ProviderUserId)
        {
            this.Email = Email;
            this.Name = Name;
            this.Picture = Picture;
            this.ProviderUserId = ProviderUserId;
        }
    }
  
}

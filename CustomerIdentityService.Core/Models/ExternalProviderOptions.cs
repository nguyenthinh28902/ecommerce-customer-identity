using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Models
{
    public class ExternalProviderOptions
    {
        public List<string> Schemes { get; set; } = new List<string>();
    }
}

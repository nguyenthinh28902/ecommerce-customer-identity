using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Core.Abstractions.Interfaces.Security
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string? Email { get; }
    }
}

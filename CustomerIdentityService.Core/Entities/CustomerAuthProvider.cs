using System;
using System.Collections.Generic;

namespace CustomerIdentityService.Infrastructure;

public partial class CustomerAuthProvider
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Provider { get; set; } = null!;

    public string ProviderUserId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}

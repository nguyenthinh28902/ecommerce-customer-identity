using System;
using System.Collections.Generic;

namespace CustomerIdentityService.Infrastructure.Persistence.Entities;

public partial class CustomerSession
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}

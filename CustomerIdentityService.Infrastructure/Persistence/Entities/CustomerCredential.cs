using System;
using System.Collections.Generic;

namespace CustomerIdentityService.Infrastructure.Persistence.Entities;

public partial class CustomerCredential
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? PasswordUpdatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}

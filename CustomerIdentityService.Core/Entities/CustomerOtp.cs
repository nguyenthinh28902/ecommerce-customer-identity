using System;
using System.Collections.Generic;

namespace CustomerIdentityService.Infrastructure;

public partial class CustomerOtp
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string OtpCode { get; set; } = null!;

    public string Purpose { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool? IsUsed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}

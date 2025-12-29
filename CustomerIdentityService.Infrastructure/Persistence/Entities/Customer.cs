using System;
using System.Collections.Generic;

namespace CustomerIdentityService.Infrastructure.Persistence.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CustomerAuthProvider> CustomerAuthProviders { get; set; } = new List<CustomerAuthProvider>();

    public virtual ICollection<CustomerCredential> CustomerCredentials { get; set; } = new List<CustomerCredential>();

    public virtual ICollection<CustomerOtp> CustomerOtps { get; set; } = new List<CustomerOtp>();

    public virtual ICollection<CustomerSession> CustomerSessions { get; set; } = new List<CustomerSession>();
}

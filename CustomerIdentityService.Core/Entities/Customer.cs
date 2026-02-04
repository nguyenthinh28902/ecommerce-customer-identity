using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomerIdentityService.Core.Entities;

[Table("CUSTOMERS")]
[Index("Email", Name = "UK_CUSTOMERS_EMAIL", IsUnique = true)]
[Index("Username", Name = "UK_CUSTOMERS_USERNAME", IsUnique = true)]
public partial class Customer
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("DISPLAY_NAME")]
    [StringLength(200)]
    public string? DisplayName { get; set; }

    [Column("AVATAR_URL")]
    [StringLength(500)]
    public string? AvatarUrl { get; set; }

    [Column("EMAIL")]
    [StringLength(256)]
    public string? Email { get; set; }

    [Column("PHONE_NUMBER")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("STATUS")]
    public byte? Status { get; set; }

    [Column("CREATED_AT", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("UPDATED_AT", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerAuthProvider> CustomerAuthProviders { get; set; } = new List<CustomerAuthProvider>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerCredential> CustomerCredentials { get; set; } = new List<CustomerCredential>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerOtp> CustomerOtps { get; set; } = new List<CustomerOtp>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerSession> CustomerSessions { get; set; } = new List<CustomerSession>();
}

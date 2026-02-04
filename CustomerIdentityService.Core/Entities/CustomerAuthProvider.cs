using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomerIdentityService.Core.Entities;

[Table("CUSTOMER_AUTH_PROVIDERS")]
[Index("Provider", "ProviderUserId", Name = "UK_PROVIDER_USER", IsUnique = true)]
public partial class CustomerAuthProvider
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CUSTOMER_ID")]
    public int CustomerId { get; set; }

    [Column("PROVIDER")]
    [StringLength(50)]
    public string Provider { get; set; } = null!;

    [Column("PROVIDER_USER_ID")]
    [StringLength(200)]
    public string ProviderUserId { get; set; } = null!;

    [Column("CREATED_AT", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerAuthProviders")]
    public virtual Customer Customer { get; set; } = null!;
}

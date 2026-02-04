using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomerIdentityService.Core.Entities;

[Table("CUSTOMER_CREDENTIALS")]
public partial class CustomerCredential
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CUSTOMER_ID")]
    public int CustomerId { get; set; }

    [Column("PASSWORD_HASH")]
    [StringLength(200)]
    public string PasswordHash { get; set; } = null!;

    [Column("PASSWORD_UPDATED_AT", TypeName = "datetime")]
    public DateTime? PasswordUpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerCredentials")]
    public virtual Customer Customer { get; set; } = null!;
}

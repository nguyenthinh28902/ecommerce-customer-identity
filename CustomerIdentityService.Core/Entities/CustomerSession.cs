using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomerIdentityService.Core.Entities;

[Table("CUSTOMER_SESSIONS")]
public partial class CustomerSession
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CUSTOMER_ID")]
    public int CustomerId { get; set; }

    [Column("REFRESH_TOKEN")]
    [StringLength(500)]
    public string RefreshToken { get; set; } = null!;

    [Column("EXPIRES_AT", TypeName = "datetime")]
    public DateTime ExpiresAt { get; set; }

    [Column("CREATED_AT", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerSessions")]
    public virtual Customer Customer { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomerIdentityService.Core.Entities;

[Table("CUSTOMER_OTP")]
public partial class CustomerOtp
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CUSTOMER_ID")]
    public int CustomerId { get; set; }

    [Column("PHONE_NUMBER")]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Column("OTP_CODE")]
    [StringLength(10)]
    public string OtpCode { get; set; } = null!;

    [Column("PURPOSE")]
    [StringLength(30)]
    public string Purpose { get; set; } = null!;

    [Column("EXPIRES_AT", TypeName = "datetime")]
    public DateTime ExpiresAt { get; set; }

    [Column("IS_USED")]
    public byte? IsUsed { get; set; }

    [Column("CREATED_AT", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerOtps")]
    public virtual Customer Customer { get; set; } = null!;
}

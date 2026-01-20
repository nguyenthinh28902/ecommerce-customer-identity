using System;
using System.Collections.Generic;
using CustomerIdentityService.Core;
using CustomerIdentityService.Core.Models.Settings;
using CustomerIdentityService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CustomerIdentityService.Infrastructure.Persistence.DbContexts;

public partial class CustomerDbContext : DbContext
{
    public CustomerDbContext()
    {
    }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAuthProvider> CustomerAuthProviders { get; set; }

    public virtual DbSet<CustomerCredential> CustomerCredentials { get; set; }

    public virtual DbSet<CustomerOtp> CustomerOtps { get; set; }

    public virtual DbSet<CustomerSession> CustomerSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle(ConnectionStrings.CustomerAppLocal);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("CUSTOMER")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("CUSTOMERS");

            entity.HasIndex(e => e.Email, "UK_CUSTOMERS_EMAIL").IsUnique();

            entity.HasIndex(e => e.Username, "UK_CUSTOMERS_USERNAME").IsUnique();

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .HasColumnName("ID");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .HasColumnName("AVATAR_URL");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(200)
                .HasColumnName("DISPLAY_NAME");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("EMAIL");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("STATUS");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("DATE")
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<CustomerAuthProvider>(entity =>
        {
            entity.ToTable("CUSTOMER_AUTH_PROVIDERS");

            entity.HasIndex(e => new { e.Provider, e.ProviderUserId }, "UK_PROVIDER_USER").IsUnique();

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.CustomerId)
                .HasPrecision(10)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .HasColumnName("PROVIDER");
            entity.Property(e => e.ProviderUserId)
                .HasMaxLength(200)
                .HasColumnName("PROVIDER_USER_ID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAuthProviders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_AUTH_CUSTOMER");
        });

        modelBuilder.Entity<CustomerCredential>(entity =>
        {
            entity.ToTable("CUSTOMER_CREDENTIALS");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasPrecision(10)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.PasswordUpdatedAt)
                .HasColumnType("DATE")
                .HasColumnName("PASSWORD_UPDATED_AT");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerCredentials)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CRED_CUSTOMER");
        });

        modelBuilder.Entity<CustomerOtp>(entity =>
        {
            entity.ToTable("CUSTOMER_OTP");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.CustomerId)
                .HasPrecision(10)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRES_AT");
            entity.Property(e => e.IsUsed)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("IS_USED");
            entity.Property(e => e.OtpCode)
                .HasMaxLength(10)
                .HasColumnName("OTP_CODE");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Purpose)
                .HasMaxLength(30)
                .HasColumnName("PURPOSE");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerOtps)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_OTP_CUSTOMER");
        });

        modelBuilder.Entity<CustomerSession>(entity =>
        {
            entity.ToTable("CUSTOMER_SESSIONS");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.CustomerId)
                .HasPrecision(10)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRES_AT");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(500)
                .HasColumnName("REFRESH_TOKEN");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerSessions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_SESSION_CUSTOMER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

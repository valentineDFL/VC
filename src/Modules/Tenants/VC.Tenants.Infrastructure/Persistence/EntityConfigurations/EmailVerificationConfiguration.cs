using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Tenants.Entities;

namespace VC.Tenants.Infrastructure.Persistence.EntityConfigurations;

internal class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
{
    public void Configure(EntityTypeBuilder<EmailVerification> builder)
    {
        builder.HasKey(ev => ev.Id);

        builder.Property(ev => ev.TenantId)
            .IsRequired();

        builder.Property(ev => ev.Code)
            .HasMaxLength(EmailVerification.CodeMaxLenght)
            .IsRequired();

        builder.Property(ev => ev.Email)
            .IsRequired();
        
        builder.Property(ev => ev.ExpirationTime)
            .IsRequired();

        builder.Property(ev => ev.IsConfirmed)
            .IsRequired();
    }
}
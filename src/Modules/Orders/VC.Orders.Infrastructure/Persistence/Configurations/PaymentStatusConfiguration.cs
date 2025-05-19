using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Orders.Payments;

namespace VC.Orders.Infrastructure.Persistence.Configurations;

internal class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
{
    public void Configure(EntityTypeBuilder<PaymentStatus> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PaymentId)
            .IsRequired();

        builder.Property(p => p.State)
            .IsRequired();

        builder.Property(p => p.CreatedOnUtc)
            .IsRequired();
    }
}
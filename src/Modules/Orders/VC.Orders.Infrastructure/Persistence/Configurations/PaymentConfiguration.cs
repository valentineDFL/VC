using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Orders.Payments;

namespace VC.Orders.Infrastructure.Persistence.Configurations;

internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.OrderId)
            .IsRequired();

        builder.OwnsMany(p => p.PaymentStatuses, psBuilder =>
        {
            psBuilder.HasKey(ps => ps.Id);

            psBuilder.Property(ps => ps.PaymentId)
                .IsRequired();

            psBuilder.Property(ps => ps.State)
                .IsRequired();

            psBuilder.Property(ps => ps.CreatedOnUtc)
                .IsRequired();
        });

        builder.Property(p => p.State)
            .IsRequired();

        builder.Property(p => p.CreatedOnUtc)
            .IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Orders.Orders;

namespace VC.Orders.Infrastructure.Persistence.Configurations;

internal class OrderIdempotencyConfiguration : IEntityTypeConfiguration<OrderIdempotency>
{
    public void Configure(EntityTypeBuilder<OrderIdempotency> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.OrderId)
            .IsRequired();

        builder.Property(oi => oi.Key)
            .HasMaxLength(OrderIdempotency.KeyLength)
            .IsRequired();

        builder.Property(oi => oi.Status)
            .IsRequired();

        builder.Property(oi => oi.CreatedOnUtc)
            .IsRequired();
    }
}
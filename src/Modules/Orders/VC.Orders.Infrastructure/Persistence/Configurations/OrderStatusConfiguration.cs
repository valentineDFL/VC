using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Orders.Orders;

namespace VC.Orders.Infrastructure.Persistence.Configurations;

internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderId)
            .IsRequired();

        builder.Property(o => o.State)
            .IsRequired();

        builder.Property(o => o.CreatedOnUtc)
            .IsRequired();
    }
}
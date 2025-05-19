using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Orders.Orders;

namespace VC.Orders.Infrastructure.Persistence.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Price)
            .IsRequired();

        builder.Property(o => o.ServiceId)
            .IsRequired();

        builder.Property(o => o.EmployeeId)
            .IsRequired();

        builder.Property(o => o.State)
            .IsRequired();

        builder.Property(o => o.CreatedOnUtc)
            .IsRequired();

        builder.Property(o => o.FinishedOnUtc)
            .IsRequired(false);
    }
}
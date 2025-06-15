using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Core.Services;

namespace VC.Core.Infrastructure.Persistence.EntityTypeConfigurations;

internal class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderSnapshot>
{
    public void Configure(EntityTypeBuilder<OrderSnapshot> builder)
    {
        builder.HasKey(oh => oh.Id);

        builder.Property(oh => oh.OrderId)
            .IsRequired();

        builder.Property(oh => oh.EmployeesIds)
            .HasColumnType("uuid[]")
            .IsRequired();

        builder.Property(x => x.ServiceTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.HasIndex(oh => oh.ServiceTime);

        builder.Property(oh => oh.State)
            .IsRequired();
    }
}
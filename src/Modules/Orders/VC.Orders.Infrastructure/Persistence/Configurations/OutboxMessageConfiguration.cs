using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VC.Orders.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(om => om.Id);

        builder.Property(om => om.ContentType)
            .IsRequired();

        builder.Property(om => om.Content)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(om => om.OccuredOnUtc)
            .IsRequired();

        builder.Property(om => om.ProcessedOnUtc)
            .IsRequired(false);

        builder.Property(om => om.Error)
            .IsRequired(false);
    }
}
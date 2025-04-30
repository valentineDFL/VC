using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VC.Services.Infrastructure.Persistence.EntityTypeConfigurations;

internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .HasMaxLength(20);

        builder.Property(r => r.Description)
            .HasMaxLength(128);
    }
}
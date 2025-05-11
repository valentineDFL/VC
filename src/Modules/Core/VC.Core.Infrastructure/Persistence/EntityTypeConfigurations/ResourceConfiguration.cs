using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VC.Core.Infrastructure.Persistence.EntityTypeConfigurations;

internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .HasMaxLength(100);

        builder.Property(r => r.Description)
            .HasMaxLength(300);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VC.Recources.Infrastructure.Configurations;

internal class ResourceConfiguration : IEntityTypeConfiguration<VC.Recources.Resource.Domain.Entities.Resource>
{
    public void Configure(EntityTypeBuilder<Resource.Domain.Entities.Resource> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .HasMaxLength(20);

        builder.Property(r => r.Description)
            .HasMaxLength(128);

        builder.HasMany(r => r.Skills)
            .WithOne(s => s.Resource)
            .HasForeignKey(s => s.ResourceId);
    }
}

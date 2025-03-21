using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Infrastructure.Configurations;

internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
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

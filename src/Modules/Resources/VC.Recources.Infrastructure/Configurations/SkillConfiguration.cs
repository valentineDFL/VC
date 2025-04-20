using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Infrastructure.Configurations;

internal class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(64);

        builder.OwnsOne(s => s.Experience);

        builder.HasOne(r => r.Resource)
            .WithMany(r => r.Skills)
            .HasForeignKey(r => r.ResourceId)
            .IsRequired(false);
    }
}
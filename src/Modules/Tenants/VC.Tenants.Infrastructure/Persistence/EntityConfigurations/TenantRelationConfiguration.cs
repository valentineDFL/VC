using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Tenants.Entities;

namespace VC.Tenants.Infrastructure.Persistence.EntityConfigurations;

internal class TenantRelationConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired();

        builder.Property(t => t.Slug)
            .IsRequired();

        builder.OwnsOne(t => t.Config);
        
        builder.OwnsOne(t => t.ContactInfo, ci =>
        {
            ci.OwnsOne(add => add.Address);
        });

        builder.OwnsOne(t => t.WorkWeekSchedule, ws =>
        {
            ws.OwnsMany(x => x.DaysSchedule, x =>
            {
                x.WithOwner().HasForeignKey(x => x.Id);
            });
        });
    }
}
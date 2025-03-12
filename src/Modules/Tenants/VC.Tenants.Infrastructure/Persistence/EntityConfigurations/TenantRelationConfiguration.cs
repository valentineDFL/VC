using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Models;

namespace VC.Tenants.Infrastructure.ModelConfigurations
{
    internal class TenantRelationConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            // id
            builder.HasKey(t => t.Id);
            
            // name
            builder.Property(t => t.Name)
                .IsRequired();

            //slug
            builder.Property(t => t.Slug)
                .IsRequired();

            // config
            builder.OwnsOne(t => t.Config);
            
            // contact info
            builder.OwnsOne(t => t.ContactInfo);

            // WorkWeekSchedule
            builder.OwnsOne(t => t.WorkWeekSchedule, ws =>
            {
                ws.OwnsMany(x => x.DaysSchedule);
            });
        }
    }
}
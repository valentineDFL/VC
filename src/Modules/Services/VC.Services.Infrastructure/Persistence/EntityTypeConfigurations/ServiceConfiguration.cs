using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VC.Services.Infrastructure.Persistence.EntityTypeConfigurations;

internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .HasMaxLength(2000);

        builder.Property(s => s.BaseDuration)
            .HasConversion(
                v => v.Ticks,
                v => TimeSpan.FromTicks(v)
            );

        builder.Property(s => s.CategoryId)
            .HasColumnName("CategoryId");

        builder.Property(s => s.RequiredResources).HasColumnType("uuid[]");

        builder.OwnsMany(
            s => s.EmployeeAssignments, 
            employeeAssignment =>
            {
                employeeAssignment.ToTable("EmployeeAssignments");
                
                employeeAssignment.HasIndex(e => e.EmployeeId);
                employeeAssignment.Property(e => e.EmployeeId)
                    .IsRequired();
                
                employeeAssignment.Property(e => e.Price)
                    .IsRequired();
                
                employeeAssignment.Property(e => e.Duration)
                    .HasConversion(
                        v => v.Ticks,
                        v => TimeSpan.FromTicks(v)
                    )
                    .IsRequired();
            }
        );
        
        builder.Metadata.FindNavigation(nameof(Service.EmployeeAssignments))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(s => s.TenantId)
            .IsRequired();

        builder.HasIndex(s => s.TenantId);
        builder.HasIndex(s => s.CategoryId);
        builder.HasIndex(s => s.IsActive);
    }
}
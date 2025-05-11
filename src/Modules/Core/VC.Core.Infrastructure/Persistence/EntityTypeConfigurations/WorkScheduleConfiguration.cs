using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VC.Core.Employees;

namespace VC.Core.Infrastructure.Persistence.EntityTypeConfigurations;

internal class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
{
    public void Configure(EntityTypeBuilder<WorkSchedule> builder)
    {
        builder.HasKey(ws => ws.Id);

        builder.Property(ws => ws.EmployeeId).IsRequired();
        builder.Property(ws => ws.TenantId).IsRequired();
        
        builder.Property(x => x.Items)
            .HasColumnType("jsonb")
            .IsRequired();
        
        builder.OwnsMany(x => x.Exceptions, exceptionBuilder =>
        {
            exceptionBuilder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_WorkingHourException_DayOffTimes",
                    """
                    "IsDayOff" = false AND "StartTime" IS NOT NULL AND "EndTime" IS NOT NULL OR 
                                  "IsDayOff" = true AND "StartTime" IS NULL AND "EndTime" IS NULL
                    """);
              
                t.HasCheckConstraint(
                    "CK_WorkingHourException_TimeRange",
                    """
                    "IsDayOff" = true OR "StartTime" < "EndTime"
                    """);
            });
            
            exceptionBuilder.WithOwner().HasForeignKey("WorkScheduleId");
            
            exceptionBuilder.HasKey(x => x.Id);
            exceptionBuilder.Property(x => x.Id).ValueGeneratedNever();
            
            exceptionBuilder.Property(x => x.EmployeeId)
                .IsRequired();
                
            exceptionBuilder.Property(x => x.Date)
                .IsRequired()
                .HasColumnType("date");
                
            exceptionBuilder.Property(x => x.IsDayOff)
                .IsRequired();
                
            exceptionBuilder.Property(x => x.StartTime)
                .HasColumnType("time without time zone");
                
            exceptionBuilder.Property(x => x.EndTime)
                .HasColumnType("time without time zone");
                
            exceptionBuilder.Property(x => x.TenantId)
                .IsRequired();
            
            exceptionBuilder.HasIndex(x => x.EmployeeId);
            exceptionBuilder.HasIndex(x => x.Date);
            exceptionBuilder.HasIndex(x => new { x.EmployeeId, x.Date }).IsUnique();
            exceptionBuilder.HasIndex(x => x.TenantId);
        });
        
        builder.HasIndex(x => x.EmployeeId);
        builder.HasIndex(x => x.TenantId);
    }
}
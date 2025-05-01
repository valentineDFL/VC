using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VC.Services.Infrastructure.Persistence.EntityTypeConfigurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Specialisation)
            .HasMaxLength(300);
    }
}
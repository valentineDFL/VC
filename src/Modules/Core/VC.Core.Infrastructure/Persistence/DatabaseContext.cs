using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VC.Core.Common;
using VC.Core.Employees;
using VC.Core.Services;
using VC.Shared.Utilities;

namespace VC.Core.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public const string Schema = "core";

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    { }

    public DbSet<Resource> Resources { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<DomainEvent>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(Schema);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            var testResource = await context.Set<Resource>()
                .FirstOrDefaultAsync(r => r.Title == "Test", cancellationToken);
        
            if (testResource is not null)
                return;
        
            var resource = new Resource(
                Guid.CreateVersion7(),
                TenantsIds.StaticTenantId,
                "Test",
                2)
            {
                Description = "Test"
            };
        
            await context.Set<Resource>().AddAsync(resource, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        });
    }
}
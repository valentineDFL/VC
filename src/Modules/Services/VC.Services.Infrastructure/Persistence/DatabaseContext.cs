using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VC.Utilities.Resolvers;

namespace VC.Services.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public const string Schema = "services";

    private readonly ITenantResolver _tenantResolver;

    public DatabaseContext(DbContextOptions options, ITenantResolver tenantResolver)
        : base(options)
    {
        _tenantResolver = tenantResolver;
    }

    public DbSet<Resource> Resources { get; set; }

    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Resource>()
            .HasQueryFilter(r => r.TenantId == _tenantResolver.Resolve());

        modelBuilder.HasDefaultSchema(Schema);
    }
}
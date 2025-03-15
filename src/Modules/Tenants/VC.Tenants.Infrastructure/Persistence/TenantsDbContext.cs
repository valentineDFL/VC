using Microsoft.EntityFrameworkCore;
using VC.Tenants.Entities;
using VC.Utilities.Resolvers;

namespace VC.Tenants.Infrastructure.Persistence;

public class TenantsDbContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; }

    private readonly ITenantResolver _tenantResolver;

    public TenantsDbContext(DbContextOptions<TenantsDbContext> options, ITenantResolver tenantResolver) : base(options)
    {
        Database.EnsureCreated();
        _tenantResolver = tenantResolver;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tenants");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantsDbContext).Assembly);

        modelBuilder.Entity<Tenant>()
            .HasQueryFilter(t => t.Id == _tenantResolver.Resolve());
    }
}
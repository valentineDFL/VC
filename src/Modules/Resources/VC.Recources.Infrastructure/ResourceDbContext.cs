using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VC.Recources.Domain.Entities;
using VC.Utilities.Resolvers;

namespace VC.Recources.Infrastructure;

internal class ResourceDbContext : DbContext
{
    private readonly ITenantResolver _tenantResolver;

    public DbSet<Resource> Resources { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public ResourceDbContext(DbContextOptions<ResourceDbContext> options, ITenantResolver tenantResolver)
        : base(options)
    {
        _tenantResolver = tenantResolver;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("resources");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Resource>()
            .HasQueryFilter(r => r.TenantId == _tenantResolver.Resolve());
    }
}

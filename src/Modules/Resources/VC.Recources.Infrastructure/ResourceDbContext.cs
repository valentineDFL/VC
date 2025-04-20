using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VC.Recources.Domain.Entities;
using VC.Utilities.Resolvers;

namespace VC.Recources.Infrastructure;

public class ResourceDbContext : DbContext
{
    public const string ResourcesSchema = "resources";

    private readonly ITenantResolver _tenantResolver;

    public ResourceDbContext(DbContextOptions options, ITenantResolver tenantResolver)
        : base(options)
    {
        _tenantResolver = tenantResolver;
        Database.EnsureCreated();
    }

    public DbSet<Resource> Resources { get; set; }
    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Resource>()
            .HasQueryFilter(r => r.TenantId == _tenantResolver.Resolve());

        modelBuilder.HasDefaultSchema(ResourcesSchema);
    }
}
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VC.Utilities.Resolvers;

namespace VC.Recources.Infrastructure;

public class ResourceDbContext : DbContext
{
    private readonly ITenantResolver _tenantResolver;

    public DbSet<VC.Recources.Resource.Domain.Entities.Resource> Resources { get; set; }

    public DbSet<VC.Recources.Resource.Domain.Entities.Skill> Skills { get; set; }

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
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(VC.Recources.Resource.Domain.Entities.Skill).Assembly);

        modelBuilder.Entity<VC.Recources.Resource.Domain.Entities.Resource>()
            .HasQueryFilter(r => r.TenantId == _tenantResolver.Resolve());
    }
}

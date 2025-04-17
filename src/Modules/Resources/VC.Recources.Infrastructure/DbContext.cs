using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VC.Recources.Domain.Entities;
using VC.Utilities.Resolvers;

namespace VC.Recources.Infrastructure;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly ITenantResolver _tenantResolver;

    public DbContext(DbContextOptions<DbContext> options, ITenantResolver tenantResolver)
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

        modelBuilder.Entity<Resource>()
            .HasMany(r => r.Skills)
            .WithOne(s => s.Resource);
    }

    public void BeginTransaction()
        => Database.BeginTransaction();

    public void CommitTransaction()
        => Database.CommitTransaction();

    public void RollBackTransaction()
        => Database.RollbackTransaction();
}
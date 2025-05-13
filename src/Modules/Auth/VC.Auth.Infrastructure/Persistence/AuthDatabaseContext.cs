using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VC.Auth.Models;
using VC.Utilities.Resolvers;

namespace VC.Auth.Infrastructure.Persistence;

public class AuthDatabaseContext : DbContext
{
    public const string Schema = "auth";

    private readonly ITenantResolver _tenantResolver;

    public AuthDatabaseContext(DbContextOptions<AuthDatabaseContext> options, ITenantResolver tenantResolver)
        : base(options)
    {
        Database.EnsureCreated();
        _tenantResolver = tenantResolver;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<User>()
            .HasQueryFilter(u => u.TenantId == _tenantResolver.Resolve());

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();
    }
}
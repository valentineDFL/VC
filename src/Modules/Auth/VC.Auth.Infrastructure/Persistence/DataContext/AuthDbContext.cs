using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VC.Auth.Models;
using VC.Utilities.Resolvers;

namespace VC.Auth.Infrastructure.Persistence.DataContext;

public class AuthDbContext : DbContext
{
    public const string Schema = "auth";

    private readonly ITenantResolver _tenantResolver;

    public AuthDbContext(DbContextOptions<AuthDbContext> options, ITenantResolver tenantResolver)
        : base(options)
    {
        _tenantResolver = tenantResolver;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema(Schema);
    }
}
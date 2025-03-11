using Microsoft.EntityFrameworkCore;

namespace VC.Tenants.Infrastructure.Persistence;

public class TenantsDbContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; }

    public TenantsDbContext(DbContextOptions<TenantsDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tenants");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantsDbContext).Assembly);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        base.OnConfiguring(optionsBuilder);
    }
}
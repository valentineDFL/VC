using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VC.Auth.Models;

namespace VC.Auth.Infrastructure.Persistence.DataContext;

public class AuthDbContext : DbContext
{
    public const string Schema = "auth";
    
    public AuthDbContext(DbContextOptions<AuthDbContext> options) 
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema(Schema);
    }
}
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VC.Core.Common;
using VC.Core.Employees;
using VC.Core.Services;

namespace VC.Core.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public const string Schema = "core";

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    { }

    public DbSet<Resource> Resources { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<WorkSchedule> WorkSchedules { get; set; }

    public DbSet<OrderSnapshot> OrdersHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<DomainEvent>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(Schema);
    }
}
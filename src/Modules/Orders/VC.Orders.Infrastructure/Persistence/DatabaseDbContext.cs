using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VC.Orders.Orders;
using VC.Orders.Payments;

namespace VC.Orders.Infrastructure.Persistence;

internal class DatabaseDbContext : DbContext
{
    public const string Schema = "orders";

    public DatabaseDbContext() : base()
    {
        
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderStatus> OrdersStatus { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<PaymentStatus> PaymentsStatus { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(Schema);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VC.Orders.Orders;
using VC.Orders.Payments;

namespace VC.Orders.Infrastructure.Persistence;

internal class OrdersDbContext : DbContext
{
    public const string Schema = "orders";

    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderStatus> OrdersStatuses { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<PaymentStatus> PaymentsStatuses { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
using Microsoft.EntityFrameworkCore;
using VC.Orders.Orders;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class OrdersRepository : IOrdersRepository
{
    private OrdersDbContext _dbContext;
    private DbSet<Order> _orders;

    public OrdersRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
        _orders = _dbContext.Orders;
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _orders
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task CreateAsync(Order order)
    {
        await _orders.AddAsync(order);
    }

    public Task UpdateAsync(Order order)
    {
        _orders.Update(order);

        return Task.CompletedTask;
    }
}
using Microsoft.EntityFrameworkCore;
using VC.Orders.Orders;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class OrdersStatusesRepository : IOrdersStatusesRepository
{
    private readonly OrdersDbContext _dbContext;
    private readonly DbSet<OrderStatus> _statuses;

    public OrdersStatusesRepository(OrdersDbContext ordersDbContext)
    {
        _dbContext = ordersDbContext;
        _statuses = _dbContext.OrdersStatuses;
    }

    public async Task<List<OrderStatus>> GetOrderStatusesByIdAsync(Guid orderId)
    {
        return await _statuses.Where(os => os.OrderId == orderId)
            .ToListAsync();
    }

    public async Task CreateOrderStatusAsync(OrderStatus orderStatus)
    {
        await _statuses.AddAsync(orderStatus);
    }
}
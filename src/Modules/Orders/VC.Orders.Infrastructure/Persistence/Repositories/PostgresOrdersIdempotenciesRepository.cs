using Microsoft.EntityFrameworkCore;
using VC.Orders.Orders;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class PostgresOrdersIdempotenciesRepository : IOrdersIdempotenciesRepository
{
    private readonly OrdersDbContext _dbContext;
    private readonly DbSet<OrderIdempotency> _ordersIdempotency;

    public PostgresOrdersIdempotenciesRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
        _ordersIdempotency = _dbContext.OrdersIdempotencies;
    }

    public async Task<OrderIdempotency> GetByKeyAsync(string key)
        => await _ordersIdempotency.FirstOrDefaultAsync(oi => oi.Key == key);
    
    public async Task AddAsync(OrderIdempotency orderIdempotency)
        => await _ordersIdempotency.AddAsync(orderIdempotency);

    public Task UpdateAsync(OrderIdempotency orderIdempotency)
    {
        _ordersIdempotency.Update(orderIdempotency);

        return Task.CompletedTask;
    }
}
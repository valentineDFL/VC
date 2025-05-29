using VC.Orders.Orders;

namespace VC.Orders.Repositories;

public interface IOrdersIdempotenciesRepository
{
    public Task<OrderIdempotency> GetByKeyAsync(string key);

    public Task AddAsync(OrderIdempotency orderIdempotency);

    public Task UpdateAsync(OrderIdempotency orderIdempotency);
}
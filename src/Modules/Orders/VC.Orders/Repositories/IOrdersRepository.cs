using VC.Orders.Orders;

namespace VC.Orders.Repositories;

public interface IOrdersRepository
{
    public Task<Order> GetByIdAsync(Guid id);

    public Task CreateAsync(Order order);

    public Task UpdateAsync(Order order);
}
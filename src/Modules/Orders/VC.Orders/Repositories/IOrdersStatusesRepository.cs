using VC.Orders.Orders;

namespace VC.Orders.Repositories;

public interface IOrdersStatusesRepository
{
    public Task<List<OrderStatus>> GetOrderStatusesByIdAsync(Guid orderId);

    public Task CreateOrderStatusAsync(OrderStatus orderStatus);
}
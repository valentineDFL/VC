using VC.Orders.Payments;

namespace VC.Orders.Repositories;

public interface IPaymentsRepository
{
    public Task<Payment> GetByOrderIdAsync(Guid orderId);

    public Task AddAsync(Payment payment);

    public Task UpdateAsync(Payment payment);
}
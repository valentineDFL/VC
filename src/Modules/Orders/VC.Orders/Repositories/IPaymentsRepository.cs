using VC.Orders.Payments;

namespace VC.Orders.Repositories;

public interface IPaymentsRepository
{
    public Task<Payment> GetByOrderIdAsync(Guid orderId);

    public Task CreatePaymentAsync(Payment payment);

    public Task UpdatePaymentAsync(Payment payment);
}
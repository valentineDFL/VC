using VC.Orders.Payments;

namespace VC.Orders.Repositories;

public interface IPaymentsStatusesRepository
{
    public Task<List<PaymentStatus>> GetByPaymentIdAsync(Guid paymentId);

    public Task CreateAsync(PaymentStatus paymentStatus);
}
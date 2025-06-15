using VC.Orders.Common;

namespace VC.Orders.Payments;

public class PaymentStatus : Entity<Guid>
{
    public PaymentStatus(Guid id, Guid paymentId, PaymentState state) : base(id)
    {
        PaymentId = paymentId;
        State = state;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid PaymentId { get; private set; }

    public PaymentState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}
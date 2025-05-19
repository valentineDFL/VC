namespace VC.Orders.Payments;

public class PaymentStatus
{
    public PaymentStatus(Guid id, Guid paymentId, PaymentState state)
    {
        Id = id;
        PaymentId = paymentId;
        State = state;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid PaymentId { get; private set; }

    public PaymentState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}
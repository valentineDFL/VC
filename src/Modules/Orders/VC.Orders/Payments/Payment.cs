using System.Text;
using VC.Orders.Orders;

namespace VC.Orders.Payments;

public class Payment
{
    private List<PaymentStatus> _paymentStatuses;

    public Payment(Guid id, Guid orderId, Order order)
    {
        var errors = new StringBuilder();

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (orderId == Guid.Empty)
            errors.AppendLine("OrderId cannot be empty");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        Id = id;
        OrderId = orderId;
        Order = order!;
        State = PaymentState.Initialed;
        CreatedOnUtc = DateTime.UtcNow;

        _paymentStatuses = new List<PaymentStatus>();
        _paymentStatuses.Add(new PaymentStatus(Guid.CreateVersion7(), Id, State));
    }

    protected Payment() { }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Order Order { get; private set; }

    public IReadOnlyList<PaymentStatus> PaymentStatuses => _paymentStatuses;

    public PaymentState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public void ChangeState(PaymentState state)
    {
        if (State == PaymentState.Canceled)
            throw new InvalidOperationException("State is cancel now");

        if (State == PaymentState.Refunded)
            throw new InvalidOperationException("State is refunded now");

        if (state == PaymentState.Initialed)
            throw new ArgumentException("State cannot be Initialed");

        State = state;
        _paymentStatuses.Add(new PaymentStatus(Guid.CreateVersion7(), Id, State));
    }
}
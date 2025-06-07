using FluentResults;
using System.Text;
using VC.Orders.Common;
using VC.Orders.Orders;

namespace VC.Orders.Payments;

public class Payment : AggregateRoot<Guid>
{
    private List<PaymentStatus> _paymentStatuses;

    public Payment(Guid id, Guid orderId, Order order) : base(id)
    {
        var errors = new StringBuilder();

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (orderId == Guid.Empty)
            errors.AppendLine("OrderId cannot be empty");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        OrderId = orderId;
        Order = order!;
        State = PaymentState.Initialed;
        CreatedOnUtc = DateTime.UtcNow;

        _paymentStatuses = new List<PaymentStatus>();
        _paymentStatuses.Add(new PaymentStatus(Guid.CreateVersion7(), Id, State));
    }

    private Payment(Guid id) : base(id)
    { }

    public Guid OrderId { get; private set; }

    public Order Order { get; private set; }

    public IReadOnlyList<PaymentStatus> PaymentStatuses => _paymentStatuses;

    public PaymentState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public Result ChangeState(PaymentState state)
    {
        if (State is PaymentState.Canceled ||
            State is PaymentState.Refunded ||
            state is PaymentState.Initialed)
            return Result.Fail($"State is {State} now");

        State = state;
        _paymentStatuses.Add(new PaymentStatus(Guid.CreateVersion7(), Id, State));

        return Result.Ok();
    }
}
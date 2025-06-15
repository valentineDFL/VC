using FluentResults;
using System.Text;
using VC.Orders.Common;
using VC.Orders.Payments;

namespace VC.Orders.Orders;

public class Order : AggregateRoot<Guid>
{
    private List<OrderStatus> _orderStatuses;

    public Order(Guid id, DateTime serviceTime, decimal price, Guid serviceId, Guid employeeId, Payment payment) : base(id)
    {
        var errors = new StringBuilder();

        if (price < 0)
            errors.AppendLine("Price cannot be non positive");

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (serviceTime < DateTime.UtcNow)
            errors.AppendLine("Service time cannot be in the past");

        if (serviceId == Guid.Empty)
            errors.AppendLine("ServiceId cannot be empty");

        if (employeeId == Guid.Empty)
            errors.AppendLine("EmployeeId cannot be empty");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        Price = price;
        ServiceTime = serviceTime;
        ServiceId = serviceId;
        EmployeeId = employeeId;
        Payment = payment!;
        State = OrderState.Created;
        CreatedOnUtc = DateTime.UtcNow;

        _orderStatuses = new List<OrderStatus>();
        _orderStatuses.Add(new OrderStatus(Guid.CreateVersion7(), Id, State));
    }

    private Order(Guid id) : base(id)
    { }

    public decimal Price { get; private set; }

    public DateTime ServiceTime { get; private set; }

    public Guid ServiceId { get; private set; }

    public Guid EmployeeId { get; private set; }

    public Payment Payment { get; private set; }

    public IReadOnlyList<OrderStatus> OrderStatuses => _orderStatuses;

    public OrderState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? FinishedOnUtc { get; private set; }

    public Result CancelOrder()
    {
        if (State is OrderState.Paid ||
            State is OrderState.Canceled ||
            State is OrderState.Refunded)
            return Result.Fail(new Error($"{State} Order cannot change State to {OrderState.Canceled}"));

        State = OrderState.Canceled;

        _orderStatuses.Add(new OrderStatus(Guid.CreateVersion7(), Id, OrderState.Canceled));

        return Result.Ok();
    }

    public Result ApplyOrderPayment()
    {
        if (State is OrderState.Paid ||
            State is OrderState.Refunded ||
            State is OrderState.Canceled ||
            State is OrderState.Paused)
            return Result.Fail($"Order with {State} state cannot be paid");

        State = OrderState.Paid;
        FinishedOnUtc = DateTime.UtcNow;

        _orderStatuses.Add(new OrderStatus(Guid.CreateVersion7(), Id, State));

        return Result.Ok();
    }
}
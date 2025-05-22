using FluentResults;
using System.Text;
using VC.Orders.Payments;

namespace VC.Orders.Orders;

public class Order
{
    private List<OrderStatus> _orderStatuses;

    public Order(Guid id, DateTime serviceTime, decimal price, Guid serviceId, Guid employeeId, Payment payment)
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

        Id = id;
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

    protected Order() { }

    public Guid Id { get; private set; }

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
        var errors = new List<IError>();

        if (State == OrderState.Paid)
            errors.Add(new Error("Paid Order cannot change State"));

        if(State == OrderState.Canceled)
            errors.Add(new Error("Canceled Order cannot change State"));

        if(State == OrderState.Refunded)
            errors.Add(new Error("Refunded Order cannot change State"));

        if (errors.Count > 0)
            return Result.Fail(errors);

        State = OrderState.Canceled;

        _orderStatuses.Add(new OrderStatus(Guid.CreateVersion7(), Id, State));

        return Result.Ok();
    }
}
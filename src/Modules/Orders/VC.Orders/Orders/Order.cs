using FluentResults;
using System.Text;
using VC.Orders.Payments;

namespace VC.Orders.Orders;

public class Order
{
    private List<OrderStatus> _orderStatuses;

    public Order(Guid id, decimal price, Guid serviceId, Guid employeeId, Payment payment)
    {
        var errors = new StringBuilder();

        if (price < 0)
            errors.AppendLine("Price cannot be non positive");

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (serviceId == Guid.Empty)
            errors.AppendLine("ServiceId cannot be empty");

        if (employeeId == Guid.Empty)
            errors.AppendLine("EmployeeId cannot be empty");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        Id = id;
        Price = price;
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

    public Guid ServiceId { get; private set; }

    public Guid EmployeeId { get; private set; }

    public Payment Payment { get; private set; }

    public IReadOnlyList<OrderStatus> OrderStatuses => _orderStatuses;

    public OrderState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? FinishedOnUtc { get; private set; }

    public Result Update(OrderState state, Guid employeeId, decimal price)
    {
        var errors = new List<IError>();

        if (state != State && State == OrderState.Canceled)
            errors.Add(new Error("Canceled Order cannot change State"));

        if (errors.Count > 0)
            return Result.Fail(errors);

        if(state == OrderState.Accepted)
            FinishedOnUtc = DateTime.UtcNow;

        if(state != State)
            State = state;

        if(EmployeeId != employeeId)
            EmployeeId = employeeId;

        if(Price != price)
            Price = price;

        _orderStatuses.Add(new OrderStatus(Guid.CreateVersion7(), Id, State));

        return Result.Ok();
    }
}
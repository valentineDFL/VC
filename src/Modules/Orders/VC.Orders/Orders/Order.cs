using FluentResults;
using System.Text;
using VC.Orders.Payments;

namespace VC.Orders.Orders;

public class Order
{
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

        if (payment is null)
            errors.AppendLine("Payment cannot be null");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        Id = id;
        Price = price;
        ServiceId = serviceId;
        EmployeeId = employeeId;
        Payment = payment!;
        State = OrderState.Created;
        CreatedOnUtc = DateTime.UtcNow;
    }

    protected Order() { }

    public Guid Id { get; private set; }

    public decimal Price { get; private set; }

    public Guid ServiceId { get; private set; }

    public Guid EmployeeId { get; private set; }

    public Payment Payment { get; private set; }

    public OrderState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? FinishedOnUtc { get; private set; }

    public Result ChangeState(OrderState newState)
    {
        if (State == OrderState.Canceled)
            return Result.Fail("Canceled Order cannot change State");

        if (newState == State)
            return Result.Fail("States cannot repeats");

        if(newState == OrderState.Accepted)
            FinishedOnUtc = DateTime.UtcNow;

        State = newState;

        return Result.Ok();
    }

    public Result ChangeEmployee(Guid newEmployeeId, decimal newPrice)
    {
        return Result.Ok();
    }
}
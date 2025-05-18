using FluentResults;
using VC.Order;

namespace VC.Orders;

public class Order
{
    public Order(Guid id, decimal price, Guid serviceId, Guid employeeId)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be non positive");

        if(id == Guid.Empty)

        Id = id;
        Price = price;
        ServiceId = serviceId;
        EmployeeId = employeeId;
        State = OrderState.Created;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public decimal Price { get; private set; }

    public Guid ServiceId { get; private set; }

    public Guid EmployeeId { get; private set; }

    public OrderState State { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public Result ChangeState(OrderState newState)
    {
        if (State == OrderState.Canceled)
            return Result.Fail("Canceled Order cannot change State");

        if (newState == State)
            return Result.Fail("States cannot repeats");

        State = newState;

        return Result.Ok();
    }
}
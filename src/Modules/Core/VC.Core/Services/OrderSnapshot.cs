using FluentResults;
using VC.Core.Common;

namespace VC.Core.Services;

public class OrderSnapshot : AggregateRoot<Guid>
{
    private List<Guid> _employeeIds;

    public OrderSnapshot(Guid id, 
                        Guid orderId, 
                        List<Guid> employeesIds,
                        DateTime serviceTime, 
                        OrderState orderState) : base(id)
    {
        Id = id;
        OrderId = orderId;
        _employeeIds = employeesIds;
        EmployeesIds = _employeeIds;
        ServiceTime = serviceTime;
        State = orderState;
    }

    public OrderSnapshot(Guid id) : base(id) { }

    public Guid OrderId { get; private set; }

    public IReadOnlyList<Guid> EmployeesIds { get; private set; }

    public DateTime ServiceTime { get; private set; }

    public OrderState State { get; private set; }

    public Result ChangeOrderState(OrderState state)
    {
        if (State is OrderState.Canceled &&
           state is OrderState.Pending)
            return Result.Fail($"{State} Order cannot change state to {state}");

        State = state;

        return Result.Ok();
    }
}
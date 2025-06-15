using VC.Core.Services;
using VC.Shared.Utilities.OrdersModuleDtos;

namespace VC.Core.Application;

public class OrderSnapshotFactory
{
    public OrderSnapshot CreateByOrderDetail(OrderDetailsDto dto)
    {
        var id = Guid.CreateVersion7();
        var orderId = dto.Id;

        var employeesIds = new List<Guid>();
        employeesIds.Add(dto.EmployeeId);

        var initedState = Services.OrderState.Pending;

        var serviceTime = dto.ServiceTime;

        return new OrderSnapshot(id,
                                orderId,
                                employeesIds,
                                serviceTime,
                                initedState);
    }
}
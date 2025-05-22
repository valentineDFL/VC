namespace VC.Orders.Application.Dtos.Create;

public record CreateOrderParams(Guid ServiceId, DateTime ServiceTime, Guid EmployeeId);
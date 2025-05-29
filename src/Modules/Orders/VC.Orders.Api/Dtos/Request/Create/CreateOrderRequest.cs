namespace VC.Orders.Api.Dtos.Request.Create;

public record CreateOrderRequest(Guid ServiceId, DateTime ServiceTime, Guid EmployeeId);
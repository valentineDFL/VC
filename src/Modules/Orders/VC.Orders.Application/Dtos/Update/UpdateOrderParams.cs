using VC.Orders.Orders;

namespace VC.Orders.Application.Dtos.Update;

public record UpdateOrderParams(Guid OrderId, OrderState State);
using VC.Orders.Orders;

namespace VC.Orders.Api.Dtos.Request.Update;

public record UpdateOrderRequest(OrderState state);
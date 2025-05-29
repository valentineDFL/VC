namespace VC.Orders.Api.Dtos.Request;

public record class PayOrderRequest(string mockCardNumbers, int cvv);
namespace VC.Auth.Application.Models.Requests;

public class CreateOrderRequest
{
    public Guid ServiceId { get; set; }

    public Guid SlotId { get; set; }

    public string? Notes { get; set; }
}
namespace VC.Auth.Application.Models.Requests;

public class CreateServiceRequest
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }
}
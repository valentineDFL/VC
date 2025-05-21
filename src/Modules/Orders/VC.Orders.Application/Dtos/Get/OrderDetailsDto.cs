namespace VC.Orders.Application.Dtos.Get;

public class OrderDetailsDto
{
    public OrderDetailsDto(Guid id, ServiceDetailDto service, decimal price)
    {
        Id = id;
        Service = service;
        Price = price;
    }

    public Guid Id { get; private set; }

    public ServiceDetailDto Service { get; private set; }

    public decimal Price { get; private set; }
}
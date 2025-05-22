namespace VC.Orders.Application.Dtos.Get;

public class OrderDetailsDto
{
    public OrderDetailsDto(Guid id, ServiceDetailDto service, decimal price, DateTime serviceTime)
    {
        Id = id;
        Service = service;
        Price = price;
        ServiceTime = serviceTime;
    }

    public Guid Id { get; private set; }

    public ServiceDetailDto Service { get; private set; }

    public decimal Price { get; private set; }

    public DateTime ServiceTime { get; private set; }
}
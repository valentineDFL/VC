namespace VC.Orders.Application.Dtos.Get;

public class ServiceDetailDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public TimeSpan BaseDuration { get; set; }

    public CategoryDetailDto? Category { get; set; }
}
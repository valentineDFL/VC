namespace VC.Orders.Application.Dtos.Get;

public class ServiceDetailDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    //public string Description { get; set; }

    // public decimal BasePrice { get; set; }

    public TimeSpan BaseDuration { get; set; }

    public CategoryDetailDto? Category { get; set; }

    //public IReadOnlyList<ResourceDto> RequiredResources { get; set; }

    //public IReadOnlyList<EmployeeDetailDto> EmployeeAssignments { get; set; }
}
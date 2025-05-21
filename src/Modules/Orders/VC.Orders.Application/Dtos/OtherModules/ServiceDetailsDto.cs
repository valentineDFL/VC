namespace VC.Orders.Application.Dtos.OtherModules;

public class ServiceDetailsDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal BasePrice { get; set; }

    public TimeSpan BaseDuration { get; set; }

    public CategoryDto? Category { get; set; }

    public bool IsActive { get; set; }

    public List<ResourceDto> RequiredResources { get; set; } = new();

    public List<EmployeeAssignmentDto> EmployeeAssignments { get; set; } = new();
}
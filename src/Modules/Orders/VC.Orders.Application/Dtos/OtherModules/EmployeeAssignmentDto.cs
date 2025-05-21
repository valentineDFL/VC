namespace VC.Orders.Application.Dtos.OtherModules;

public class EmployeeAssignmentDto
{
    public Guid Id { get; set; }

    public Guid EmployeeId { get; set; }

    public decimal Price { get; set; }

    public TimeSpan Duration { get; set; }
}
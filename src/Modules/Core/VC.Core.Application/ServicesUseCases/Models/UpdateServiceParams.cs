namespace VC.Core.Application.ServicesUseCases.Models;

public record UpdateServiceParams(
    Guid Id,
    string Title,
    decimal BasePrice,
    TimeSpan BaseDuration,
    string? Description = null,
    Guid? CategoryId = null,
    List<Guid>? RequiredResources = null,
    List<UpdateServiceParams.EmployeeAssignmentDto>? EmployeeAssignments = null)
{
    public class EmployeeAssignmentDto
    {
        public Guid EmployeeId { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
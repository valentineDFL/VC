namespace VC.Services.Api.Models.Services;

public record CreateServiceRequest(
    string Title,
    decimal BasePrice,
    TimeSpan BaseDuration,
    string? Description = null,
    Guid? CategoryId = null,
    List<Guid>? RequiredResources = null,
    List<CreateServiceRequest.EmployeeAssignmentDto>? EmployeeAssignments = null)
{
    public record EmployeeAssignmentDto(Guid EmployeeId, decimal Price, TimeSpan Duration);
}
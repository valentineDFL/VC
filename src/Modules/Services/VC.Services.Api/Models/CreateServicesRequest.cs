namespace VC.Services.Api.Models;

public record CreateServiceRequest(
    string Title,
    string Description,
    decimal Price,
    TimeSpan? Duration,
    bool IsActive,
    List<Guid> ResourceRequirement);
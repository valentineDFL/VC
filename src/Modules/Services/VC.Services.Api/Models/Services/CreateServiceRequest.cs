namespace VC.Services.Api.Models.Services;

public record CreateServiceRequest(
    string Title,
    string Description,
    decimal Price,
    TimeSpan? Duration,
    bool IsActive,
    List<Guid> ResourceRequirement);
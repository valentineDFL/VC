namespace VC.Services.Application.ResourcesUseCases.Models;

public record UpdateResourceParams(
    Guid Id,
    string Title,
    string Description,
    int Count);
    
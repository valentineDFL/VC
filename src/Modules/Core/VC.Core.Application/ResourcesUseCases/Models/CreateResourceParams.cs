namespace VC.Core.Application.ResourcesUseCases.Models;

public record CreateResourceParams(
    string Title,
    string Description,
    int Count);
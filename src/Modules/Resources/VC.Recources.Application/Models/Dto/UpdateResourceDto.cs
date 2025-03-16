namespace VC.Recources.Application.Models.Dto;

public record UpdateResourceDto(
    Guid Id,
    string Name,
    string Description,
    Dictionary<string, object> Attributes
    );

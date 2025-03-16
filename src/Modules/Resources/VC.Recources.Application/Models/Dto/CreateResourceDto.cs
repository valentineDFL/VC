using VC.Recources.Resource.Domain.Entities;

namespace VC.Recources.Application.Models.Dto;

public record CreateResourceDto(
    string Name,
    string Description,
    ResourceType ResourceType,
    Dictionary<string,object> Attributes
    );


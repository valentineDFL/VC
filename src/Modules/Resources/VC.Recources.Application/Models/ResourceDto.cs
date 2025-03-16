using VC.Recources.Resource.Domain.Entities;

namespace VC.Recources.Application.Models;

public record ResourceDto(
    Guid ResourceId,
    string Name,
    ResourceType ResourceType,
    string Description,
    Dictionary<string, object> Attribute
    );
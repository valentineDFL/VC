using VC.Recources.Resource.Domain.Entities;

namespace VC.Resources.Api.Endpoints.Models.Response;

public class ResourceResponseDto(
    string name,
    string description,
    ResourceType resourceType,
    Dictionary<string, object> attributes
    )
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public ResourceType ResourceType { get; set; } = resourceType;

    public Dictionary<string, object> Attributes { get; set; } = attributes;
};
using VC.Recources.Resource.Domain.Entities;

namespace VC.Resources.Api.Endpoints;

public record ResourceResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ResourceType ResourceType { get; set; }

    public Dictionary<string, object> Attributes { get; set; }
};

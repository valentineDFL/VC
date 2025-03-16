using VC.Recources.Resource.Domain.Entities;
using VC.Recources.Application.Models.Dto;

namespace VC.Resources.Api.Endpoints.Models.Request;

public record CreateResourceRequest(
    string Name,
    string Description,
    ResourceType ResourceType,
    Dictionary<string, object> Attributes
    );

public static class ResourceCreateMapper
{
    public static CreateResourceDto ToCreateResourceDto(this CreateResourceRequest dto)
    {
        return new CreateResourceDto(dto.Name,dto.Description,dto.ResourceType,dto.Attributes);
    }
}

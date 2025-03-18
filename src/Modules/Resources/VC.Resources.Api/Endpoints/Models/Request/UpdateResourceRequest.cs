using VC.Recources.Application.Models.Dto;

namespace VC.Resources.Api.Endpoints.Models.Request;

public record UpdateResourceRequest(
    Guid Id,
    string Name,
    string Description,
    Dictionary<string, object> Attributes
    );

public static class UpdateResourceMapper
{
    public static UpdateResourceDto ToUpdateResourceDto(this UpdateResourceRequest dto, Guid tenantId)
    {
        return new UpdateResourceDto(tenantId, dto.Id, dto.Name, dto.Description, dto.Attributes);
    }
}

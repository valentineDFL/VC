namespace VC.Resources.Api.Endpoints.Models.Request;

public record UpdateResourceRequest(
    Guid Id,
    string Name,
    string Description,
    Dictionary<string, object> Attributes
    );

public static class UpdateResourceMapper
{
    public static VC.Recources.Application.Models.Dto.UpdateResourceDto ToUpdateResourceDto(this UpdateResourceRequest dto)
    {
        return new VC.Recources.Application.Models.Dto.UpdateResourceDto(dto.Id, dto.Name, dto.Description, dto.Attributes);
    }
}

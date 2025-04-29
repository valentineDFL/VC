using VC.Services.Application.ResourcesUseCases.Models;

namespace VC.Services.Api.Models.Resources.UpdateResource;

public record UpdateResourceRequest(
    string Name,
    string Description);

public static class UpdateMappers
{
    public static UpdateResourceParams ToUpdateDto(this UpdateResourceRequest dto, Guid id)
        => new UpdateResourceParams(id, dto.Name, dto.Description);
}

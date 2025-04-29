using VC.Services.Application.ResourcesUseCases.Models;

namespace VC.Services.Api.Models.Resources.CreateResource;

public record CreateResourceRequest(
    string Title,
    string Description);

public static class CreateMappers
{
    public static CreateResourceParams ToCreateParams(this CreateResourceRequest dto)
        => new CreateResourceParams(dto.Title, dto.Description);
}

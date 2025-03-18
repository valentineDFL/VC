using VC.Recources.Application.Models;
using VC.Resources.Api.Endpoints.Models.Response;

namespace VC.Resources.Api.Endpoints;

internal static class ResourceResponseMapper
{
    public static ResourceResponseDto ToResponseDto(this ResourceDto dto)
    {
        return new ResourceResponseDto(dto.Name, dto.Description, dto.Skills);
    }
}

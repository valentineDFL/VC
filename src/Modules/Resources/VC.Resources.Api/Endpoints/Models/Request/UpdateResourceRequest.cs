using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Application.Endpoints.Models.Request;

namespace VC.Resources.Api.Endpoints.Models.Request;

public record UpdateResourceRequest(
    string Name,
    string Description,
    IReadOnlyList<SkillDto> Skills
    );

public static class UpdateResourceMappers
{
    public static UpdateResourceDto ToUpdateResourceDto(this UpdateResourceRequest dto, Guid tenantId)
        => new UpdateResourceDto(tenantId, dto.Name, dto.Description, dto.Skills.ToList());
}

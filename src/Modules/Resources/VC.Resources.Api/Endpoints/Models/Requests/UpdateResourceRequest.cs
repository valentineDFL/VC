using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Application.Endpoints.Models.Requests;

namespace VC.Resources.Api.Endpoints.Models.Requests;

public record UpdateResourceRequest(
    Guid Id,
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class UpdateResourceMappers
{
    public static UpdateResourceDto ToUpdateResourceDto(this UpdateResourceRequest dto, Guid tenantId)
        => new UpdateResourceDto(tenantId, dto.Name, dto.Description, dto.Skills.ToList());
}

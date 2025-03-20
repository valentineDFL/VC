using VC.Recources.Application.Models.Dto;

namespace VC.Resources.Api.Endpoints.Models.Request;

public record CreateResourceRequest(
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class ResourceCreateMapper
{
    public static CreateResourceDto ToCreateResourceDto(this CreateResourceRequest dto, Guid tenantId)
        => new CreateResourceDto(tenantId, dto.Name, dto.Description, dto.Skills.ToResourceSkill());
}

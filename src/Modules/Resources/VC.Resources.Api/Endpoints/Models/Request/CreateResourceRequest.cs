using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Application.Endpoints.Models.Request;

namespace VC.Resources.Api.Endpoints.Models.Request;

public record CreateResourceRequest(
    string Name,
    string Description,
    IReadOnlyList<SkillDto> Skills
    );

public static class CreateResourceMappers
{
    public static CreateResourceDto ToCreateResourceDto(this CreateResourceRequest dto, Guid tenantId)
        => new CreateResourceDto(tenantId, dto.Name, dto.Description, dto.Skills.ToList());
}

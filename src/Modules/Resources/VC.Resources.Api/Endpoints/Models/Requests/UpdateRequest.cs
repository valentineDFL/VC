using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Application.Endpoints.Models.Requests;

namespace VC.Resources.Api.Endpoints.Models.Requests;

public record UpdateRequest(
    Guid Id,
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class UpdateMappers
{
    public static UpdateDto ToUpdateDto(this UpdateRequest dto, Guid tenantId)
        => new UpdateDto(tenantId, dto.Name, dto.Description, dto.Skills.ToList());
}

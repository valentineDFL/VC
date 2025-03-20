using VC.Recources.Application.Models.Dto;

namespace VC.Resources.Api.Endpoints.Models.Request;

public record UpdateResourceRequest(
    Guid Id,
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class UpdateResourceMapper
{
    public static UpdateResourceDto ToUpdateResourceDto(this UpdateResourceRequest dto)
        => new UpdateResourceDto(dto.Id, dto.Name, dto.Description, dto.Skills.ToResourceSkill());
}

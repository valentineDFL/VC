using VC.Recources.Application.Models.Dto;

namespace VC.Resources.Api.Endpoints.Models.Request;

public record UpdateResourceRequest(
    Guid Id,
    string Name,
    string Description,
    IReadOnlyList<SkillDto> Skills
    );

public static class UpdateResourceMappers
{
    public static UpdateResourceDto ToUpdateResourceDto(this UpdateResourceRequest dto)
        => new UpdateResourceDto(dto.Id, dto.Name, dto.Description, dto.Skills.ToList());
}

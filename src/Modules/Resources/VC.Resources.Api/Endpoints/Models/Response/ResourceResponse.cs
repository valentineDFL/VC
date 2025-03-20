using VC.Recources.Resource.Domain.Entities;

namespace VC.Resources.Api.Endpoints.Models.Response;

public class ResourceResponse(
    string name,
    string description,
    List<SkillDto> skills
    )
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public List<SkillDto> Skills { get; set; } = skills;
};

internal static class ResourceResponseMapper
{
    public static ResourceResponse ToResponseDto(this Resource dto)
        => new ResourceResponse(dto.Name, dto.Description, dto.Skills.ToApiSkills());
}
using VC.Recources.Domain.Entities;
using VC.Resources.Api.Endpoints.Models.Dto;

namespace VC.Resources.Api.Endpoints.Models.Responses;

public class Response(
    string name,
    string description,
    List<SkillDto> skills
    )
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public List<SkillDto> Skills { get; set; } = skills;
};

internal static class ResponseMapper
{
    public static Response ToResponseDto(this Resource dto)
        => new Response(dto.Name, dto.Description, dto.Skills.ToApiSkills());
}
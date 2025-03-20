using VC.Recources.Application.Models;
using VC.Recources.Resource.Domain.Entities;

namespace VC.Resources.Api;

public class ResponseResource(
    string name,
    string description,
    List<SkillDto> skills
    )
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public List<SkillDto> Skills { get; set; } = skills;
};

public static class ResponseDtoMappers
{
    public static ResponseResource ToApiResource(this Resource dto)
        => new ResponseResource
        (
            dto.Name,
            dto.Description,
            dto.Skills.ToApiSkills()
        );
}
using VC.Recources.Resource.Domain.Entities;

namespace VC.Resources.Api.Endpoints.Models;

public record SkillDto(string Name, ExperienceDto Expirience)
{
    public string Name { get; set; } = Name;

    public ExperienceDto Expirience { get; set; } = Expirience; 
}

public static class SkillDtoMappers
{
    public static List<SkillDto> ToApiSkills(this List<Skill> skills)
        => skills
            .Select(skill => new SkillDto
            (
               skill.SkillName,
               skill.Expirience.ToApiExperienceDto()
            ))
            .ToList();

    public static List<VC.Recources.Application.Models.SkillDto> ToApplicationSkillDto(this List<SkillDto> skills)
    => skills
        .Select(skill => new VC.Recources.Application.Models.SkillDto
        (
            skill.Name,
            skill.Expirience.ToApplicationExperienceDto()
        ))
        .ToList();
}

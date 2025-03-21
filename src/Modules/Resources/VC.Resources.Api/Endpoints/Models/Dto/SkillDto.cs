using VC.Recources.Domain.Entities;

namespace VC.Resources.Api.Endpoints.Models.Dto;

public record SkillDto(Guid Id, string Name, ExperienceDto Expirience)
{
    public Guid Id { get; set; } = Id;

    public string Name { get; set; } = Name;

    public ExperienceDto Expirience { get; set; } = Expirience;
}

public static class SkillDtoMappers
{
    public static List<SkillDto> ToApiSkills(this List<Skill> skills)
        => skills
            .Select(skill => new SkillDto
            (
                skill.Id,
                skill.Name,
                skill.Experience.ToApiExperienceDto()
            ))
            .ToList();

    public static List<SkillDto> ToApplicationSkillDto(this List<SkillDto> skills)
        => skills
            .Select(skill => new SkillDto
            (
                skill.Id,
                skill.Name,
                skill.Expirience.ToApplicationExperienceDto()
            ))
            .ToList();
}

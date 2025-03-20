using VC.Recources.Resource.Domain.Entities;

namespace VC.Recources.Application.Models;

public record SkillDto(string Name, ExperienceDto Experience)
{
    public string Name { get; set; } = Name;

    public ExperienceDto Experience { get; set; } = Experience;
}

public static class SkillDtoMappers
{
    public static List<Skill> ToDomainSkills(this List<SkillDto> dtos)
        => dtos
            .Select(dto => new Skill
            {
                SkillName = dto.Name,
                Expirience = dto.Experience.ToDomainExperience()
            })
            .ToList();
}


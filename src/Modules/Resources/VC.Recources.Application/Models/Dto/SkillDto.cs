using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Models.Dto;

public record SkillDto(Guid Id, string Name, ExperienceDto Experience)
{
    public Guid Id { get; set; } = Id;

    public string Name { get; set; } = Name;

    public ExperienceDto Experience { get; set; } = Experience;
}

public static class SkillDtoMappers
{
    public static List<Skill> ToDomainSkills(this List<SkillDto> dtos)
        => dtos
            .Select(dto => new Skill
            {
                Id = dto.Id,
                Name = dto.Name,
                Experience = dto.Experience.ToDomainExperience()
            })
            .ToList();
}


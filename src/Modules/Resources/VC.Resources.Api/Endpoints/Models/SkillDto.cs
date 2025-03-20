namespace VC.Resources.Api.Endpoints.Models;

public record SkillDto(string Name)
{
    public string Name { get; set; } = Name;
}

public static class SkillDtoMappers
{
    public static List<SkillDto> ToResourceSkill(this List<SkillDto> dtos)
        => dtos
            .Select(dto => new SkillDto(dto.Name))
            .ToList();
}

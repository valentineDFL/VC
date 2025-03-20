namespace VC.Recources.Application.Models.Response;

public class ResourceDto(
    string name,
    string description,
    List<SkillDto> skills
    )
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public List<SkillDto> Skills { get; set; } = skills;
}
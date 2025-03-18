using VC.Recources.Resource.Domain.Entities;

namespace VC.Resources.Api.Endpoints.Models.Response;

public class ResourceResponseDto(
    string name,
    string description,
    List<Skill> skills
    )
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public List<Skill> Skills { get; set; } = skills;
};
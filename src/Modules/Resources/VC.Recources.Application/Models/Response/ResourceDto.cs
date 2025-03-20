using VC.Recources.Resource.Domain.Entities;

namespace VC.Recources.Application.Models.Response;

public class ResourceDto(
    Guid resourceId,
    string name,
    string description,
    List<Skill> skills
    )
{
    public Guid ResourceId { get; set; } = resourceId;

    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public List<Skill> Skills { get; set; } = skills;
}
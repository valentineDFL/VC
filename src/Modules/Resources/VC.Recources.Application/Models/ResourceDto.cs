using VC.Recources.Resource.Domain.Entities;

namespace VC.Recources.Application.Models;

public record ResourceDto(
    Guid ResourceId,
    string Name,
    string Description,
    List<Skill> Skills
    );
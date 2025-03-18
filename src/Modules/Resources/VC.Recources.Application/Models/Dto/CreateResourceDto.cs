using VC.Recources.Resource.Domain.Entities;

namespace VC.Recources.Application.Models.Dto;

public record CreateResourceDto(
    Guid TenantId,
    string Name,
    string Description,
    List<Skill> Skills
    );


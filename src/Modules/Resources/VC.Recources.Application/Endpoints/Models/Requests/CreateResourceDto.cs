using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Endpoints.Models.Requests;

public record CreateResourceDto(
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class CreateResourceMappers
{
    public static Resource ToResourceDomain(this CreateResourceDto dto)
        => new Resource
        {
            Name = dto.Name,
            Description = dto.Description,
            Skills = dto.Skills.ToDomainSkills()
        };
}
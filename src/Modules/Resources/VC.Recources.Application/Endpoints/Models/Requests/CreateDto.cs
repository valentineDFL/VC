using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Endpoints.Models.Requests;

public record CreateDto(
    string Name,
    string Description,
    List<SkillDto> Skills
);

public static class CreateMappers
{
    public static Resource ToDomain(this CreateDto dto)
        => new Resource
        {
            Name = dto.Name,
            Description = dto.Description,
            Skills = dto.Skills.ToDomainSkills()
        };
}
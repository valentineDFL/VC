using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Endpoints.Models.Request;

public record UpdateResourceDto(
    Guid Id,
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class UpdateResourceMappers
{
    public static Resource ToResourceDomain(this UpdateResourceDto dto)
       => new Resource
       {
           Id = dto.Id,
           Name = dto.Name,
           Description = dto.Description,
           Skills = dto.Skills.ToDomainSkills()
       };
}
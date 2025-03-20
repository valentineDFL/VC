namespace VC.Recources.Application.Models.Dto;

public record UpdateResourceDto(
    Guid Id,
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class UpdateResourceMappers
{
    public static VC.Recources.Resource.Domain.Entities.Resource ToResourceDomain(this UpdateResourceDto dto)
       => new VC.Recources.Resource.Domain.Entities.Resource
       {
           Name = dto.Name,
           Description = dto.Description,
           Skills = dto.Skills.ToDomainSkills()
       };
}
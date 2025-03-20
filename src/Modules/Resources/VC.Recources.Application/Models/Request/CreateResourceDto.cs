namespace VC.Recources.Application.Models.Dto;

public record CreateResourceDto(
    Guid TenantId,
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class CreateResourceMappers
{
    public static VC.Recources.Resource.Domain.Entities.Resource ToResourceDomain(this CreateResourceDto dto)
        => new VC.Recources.Resource.Domain.Entities.Resource
        {
            Name = dto.Name,
            Description = dto.Description,
            Skills = dto.Skills.ToDomainSkills()
        };
}
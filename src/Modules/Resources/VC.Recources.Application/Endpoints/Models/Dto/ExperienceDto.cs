using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Endpoints.Models.Dto;

public record ExperienceDto(int Years, int Months)
{
    public int Years { get; set; } = Years;
    public int Months { get; set; } = Months;
}

public static class ExpirienceSkillDtoMappers
{
    public static Experience ToDomainExperience(this ExperienceDto dto)
        => new Experience
        {
            Years = dto.Years,
            Months = dto.Months
        };
}
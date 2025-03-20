using VC.Recources.Resource;

namespace VC.Recources.Application.Models;

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

    public static ExperienceDto ToApiExperience(this Experience experience)
        => new ExperienceDto
        (
            experience.Years,
            experience.Months
        );
}
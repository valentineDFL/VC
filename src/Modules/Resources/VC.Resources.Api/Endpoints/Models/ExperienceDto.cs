using VC.Recources.Resource;

namespace VC.Resources.Api.Endpoints.Models;

public class ExperienceDto(int Years, int Month)
{
    public int Years { get; set; } = Years;
    public int Months { get; set; } = Month;
};

public static class ExperienceDtoMappers
{
    public static Experience ToDomainExperience(this ExperienceDto dto)
        => new Experience
        {
            Years = dto.Years,
            Months = dto.Months
        };

    public static ExperienceDto ToExperienceDto(this Experience experience)
        => new ExperienceDto
        (
            experience.Months,
            experience.Years
        );
}

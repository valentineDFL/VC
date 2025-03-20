using VC.Recources.Resource;

namespace VC.Resources.Api.Endpoints.Models;

public class ExperienceDto(int Years, int Month)
{
    public int Years { get; set; } = Years;
    public int Months { get; set; } = Month;
};

public static class ExperienceDtoMappers
{
    public static VC.Recources.Application.Models.ExperienceDto ToApplicationExperienceDto(this ExperienceDto dto)
        => new VC.Recources.Application.Models.ExperienceDto
        (
            dto.Years,
            dto.Months
        );

    public static ExperienceDto ToApiExperienceDto(this Experience experience)
        => new ExperienceDto
        (
            experience.Months,
            experience.Years
        );
}

using Mapster;

namespace VC.Tenants.Api.Models.Request.DaySchedule;

public record CreateDayScheduleDto(DayOfWeek Day, DateTime StartWork, DateTime EndWork);

internal class CreateDayScheduleDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<CreateDayScheduleDto, Application.Models.Create.CreateDayScheduleDto>.NewConfig()
            .Map(dest => dest.Day, src => src.Day)
            .Map(dest => dest.StartWork, src => src.StartWork)
            .Map(dest => dest.EndWork, src => src.EndWork);

        TypeAdapterConfig<IReadOnlyList<CreateDayScheduleDto>, IReadOnlyList<Application.Models.Create.CreateDayScheduleDto>>.NewConfig()
            .Map(dest => dest, src => src);
    }
}
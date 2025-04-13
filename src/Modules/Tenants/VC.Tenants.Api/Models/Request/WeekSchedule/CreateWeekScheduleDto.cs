using Mapster;
using VC.Tenants.Api.Models.Request.DaySchedule;

namespace VC.Tenants.Api.Models.Request.WeekSchedule;

public record CreateWeekScheduleDto(IReadOnlyList<CreateDayScheduleDto> WeekSchedule);

internal class CreateWeekScheduleDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<CreateWeekScheduleDto, Application.Models.Create.CreateWeekScheduleDto>.NewConfig()
            .Map(dest => dest.ScheduleDays, src => src.WeekSchedule);
    }
}
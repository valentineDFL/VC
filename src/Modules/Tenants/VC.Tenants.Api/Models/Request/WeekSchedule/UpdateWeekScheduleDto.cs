using Mapster;
using VC.Tenants.Api.Models.Request.DaySchedule;

namespace VC.Tenants.Api.Models.Request.WeekSchedule;

public record UpdateWeekScheduleDto(IReadOnlyList<UpdateDayScheduleDto> WeekDays);

internal class UpdateWorkWeekScheduleDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<UpdateWeekScheduleDto, Application.Models.Update.UpdateScheduleWeekDto>.NewConfig()
            .Map(dest => dest.ScheduleDayDtos, src => src.WeekDays);
    }
}
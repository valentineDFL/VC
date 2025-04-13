using Mapster;

namespace VC.Tenants.Api.Models.Request.DaySchedule;

public record UpdateDayScheduleDto(Guid Id, DayOfWeek Day, DateTime StartWork, DateTime EndWork);

internal class UpdateDayScheduleDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<UpdateDayScheduleDto, Application.Models.Update.UpdateScheduleDayDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Day, src => src.Day)
            .Map(dest => dest.StartWork, src => src.StartWork)
            .Map(dest => dest.EndWork, src => src.EndWork);

        TypeAdapterConfig<IReadOnlyList<UpdateDayScheduleDto>, IReadOnlyList<Application.Models.Update.UpdateScheduleDayDto>>.NewConfig()
            .Map(dest => dest, src => src);
    }
}
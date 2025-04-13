using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Update;

public class UpdateScheduleWeekDto(IReadOnlyList<UpdateScheduleDayDto> weekDays)
{
    public IReadOnlyList<UpdateScheduleDayDto> ScheduleDayDtos { get; } = weekDays;
}

public static class UpdateScheduleWeekDtoMapper
{
    public static TenantWeekSchedule ToEntity(this UpdateScheduleWeekDto dto)
        => TenantWeekSchedule.Create(dto.ScheduleDayDtos.ToEntities().ToList());
}
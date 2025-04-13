using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Update;

public class UpdateScheduleDayDto(Guid id, DayOfWeek day, DateTime startWork, DateTime endWork)
{
    public Guid Id { get; } = id;

    public DayOfWeek Day { get; } = day;

    public DateTime StartWork { get; } = startWork;

    public DateTime EndWork { get; set; } = endWork;
}

internal static class UpdateScheduleDayDtoMapper
{
    public static TenantDaySchedule ToEntity(this UpdateScheduleDayDto dto)
        => TenantDaySchedule.Create(dto.Id, dto.Day, dto.StartWork, dto.EndWork);

    public static IReadOnlyList<TenantDaySchedule> ToEntities(this IReadOnlyList<UpdateScheduleDayDto> dtos)
        => dtos.Select(dto => TenantDaySchedule.Create(dto.Id, dto.Day, dto.StartWork, dto.EndWork)).ToList();
}
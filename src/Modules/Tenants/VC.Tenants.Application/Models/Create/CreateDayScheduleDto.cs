using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Create;

public class CreateDayScheduleDto(DayOfWeek day, DateTime startWork, DateTime endWork)
{
    public DayOfWeek Day { get; } = day;

    public DateTime StartWork { get; } = startWork;

    public DateTime EndWork { get; } = endWork;
}

public static class CreateDayScheduleDtoMapper
{
    public static TenantDaySchedule ToEntity(this CreateDayScheduleDto dto)
        => TenantDaySchedule.Create(Guid.CreateVersion7(), dto.Day, dto.StartWork, dto.EndWork);

    public static IReadOnlyList<TenantDaySchedule> ToEntities(this IReadOnlyList<CreateDayScheduleDto> dtos)
        => dtos.Select(dto => dto.ToEntity()).ToList();
}
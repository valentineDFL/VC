using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Create;

public class CreateWeekScheduleDto(IReadOnlyList<CreateDayScheduleDto> dtos)
{
    public IReadOnlyList<CreateDayScheduleDto> ScheduleDays { get; } = dtos;
}

internal static class CreateWeekScheduleDtoMapper
{
    public static TenantWeekSchedule ToEntity(this CreateWeekScheduleDto dto)
        => TenantWeekSchedule.Create(dto.ScheduleDays.ToEntities().ToList());
}
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Create;

public record CreateDayScheduleDto(DayOfWeek Day, DateTime StartWork, DateTime EndWork);

public static class CreateDayScheduleDtoMapper
{
    public static DaySchedule ToEntity(this CreateDayScheduleDto dto, Guid tenantId)
        => DaySchedule.Create(Guid.CreateVersion7(), tenantId, dto.Day, dto.StartWork, dto.EndWork);

    public static IReadOnlyList<DaySchedule> ToEntities(this IReadOnlyList<CreateDayScheduleDto> dtos, Guid tenantId)
        => dtos.Select(dto => dto.ToEntity(tenantId)).ToList();
}
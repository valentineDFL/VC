using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Update;

public record UpdateScheduleDayDto(Guid Id, DayOfWeek Day, DateTime StartWork, DateTime EndWork);

internal static class UpdateScheduleDayDtoMapper
{
    public static DaySchedule ToEntity(this UpdateScheduleDayDto dto, Guid tenantId)
        => DaySchedule.Create(dto.Id, tenantId, dto.Day, dto.StartWork, dto.EndWork);

    public static IReadOnlyList<DaySchedule> ToEntities(this IReadOnlyList<UpdateScheduleDayDto> dtos, Guid tenantId)
        => dtos.Select(dto => DaySchedule.Create(dto.Id, tenantId, dto.Day, dto.StartWork, dto.EndWork)).ToList();
}
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants.Models;

public class TenantWeekWorkSheduleDto(IReadOnlyList<TenantDayWorkScheduleDto> workDays)
{
    public IReadOnlyList<TenantDayWorkScheduleDto> WorkDays { get; } = workDays;
}

internal static class TenantWeekWorkSheduleDtoMapper
{
    public static TenantWorkSchedule ToEntity(this TenantWeekWorkSheduleDto tenantWeekWorkSheduleDto)
        => TenantWorkSchedule.Create(tenantWeekWorkSheduleDto.WorkDays.ToEntity());
}
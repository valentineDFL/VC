using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record TenantWeekWorkScheduleDto(IReadOnlyList<TenantDayWorkScheduleDto> WeekDays);

internal static class TenantWorkScheduleMapper
{
    public static Application.Tenants.Models.TenantWeekWorkSheduleDto ToApplicationDto(this Api.Endpoints.Tenants.Models.TenantWeekWorkScheduleDto dto)
        => new Application.Tenants.Models.TenantWeekWorkSheduleDto(dto.WeekDays.ToApplicationDto());

    public static TenantWeekWorkScheduleDto ToApiDto(this TenantWorkSchedule tenantWeekWork)
        => new TenantWeekWorkScheduleDto(tenantWeekWork.DaysSchedule.ToApiDto());
}
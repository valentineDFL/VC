using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record TenantDayWorkScheduleDto(DayOfWeek Day, DateTime StartWork, DateTime EndWork);

internal static class TenantDayWorkScheduleMapper
{
    public static IReadOnlyList<VC.Tenants.Application.Tenants.Models.TenantDayWorkScheduleDto> ToApplicationDto(this IReadOnlyList<TenantDayWorkScheduleDto> dtos) =>
        [.. dtos.Select(dto => 
                new VC.Tenants.Application.Tenants.Models.TenantDayWorkScheduleDto(dto.Day, dto.StartWork, dto.EndWork))];

    public static IReadOnlyList<TenantDayWorkScheduleDto> ToApiDto(this IReadOnlyList<TenantDayWorkSchedule> tenantDayWorks) =>
        [.. tenantDayWorks.Select(tdw =>
                new TenantDayWorkScheduleDto(tdw.Day, tdw.StartWork, tdw.EndWork))];
}
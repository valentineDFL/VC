using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record TenantDayWorkScheduleDto(
    DayOfWeek Day,
    DateTime StartWork,
    DateTime EndWork
    );

public static class TenantDayWorkScheduleMappers
{
    public static List<VC.Tenants.Application.Tenants.Models.TenantDayWorkScheduleDto> ToTenantsDayWorkShedule(this List<TenantDayWorkScheduleDto> dtos) =>
        [.. dtos.Select(dto => 
                new VC.Tenants.Application.Tenants.Models.TenantDayWorkScheduleDto(dto.Day, dto.StartWork, dto.EndWork))];

    public static List<TenantDayWorkScheduleDto> ToApiDayWorkSchedule(this List<TenantDayWorkSchedule> tenantDayWorks) =>
        [.. tenantDayWorks.Select(tdw =>
                new TenantDayWorkScheduleDto(tdw.Day, tdw.StartWork, tdw.EndWork))];
}
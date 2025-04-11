using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants.Models;

public class TenantDayWorkScheduleDto(DayOfWeek day, DateTime startWork, DateTime endWork)
{
    public DayOfWeek Day { get; } = day;

    public DateTime StartWork { get; } = startWork;

    public DateTime EndWork { get; } = endWork;
}

internal static class TenantDayWorkScheduleDtoMapper
{
    public static List<TenantDayWorkSchedule> ToEntity(this IReadOnlyList<TenantDayWorkScheduleDto> tenantDayWorksDtos)
        => tenantDayWorksDtos
           .Select(tdw => TenantDayWorkSchedule.Create(tdw.Day, tdw.StartWork, tdw.EndWork))
           .ToList();
}
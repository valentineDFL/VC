using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record TenantDayWorkScheduleDto(
    DayOfWeek Day,
    DateTime StartWork,
    DateTime EndWork
    );

public static class TenantDayWorkScheduleMappers
{
    public static List<VC.Tenants.Application.Tenants.Models.TenantDayWorkScheduleDto> ToTenantsDayWorkShedule(this List<TenantDayWorkScheduleDto> dtos)
    {
        var result = new List<VC.Tenants.Application.Tenants.Models.TenantDayWorkScheduleDto>();

        foreach(var dayWorkSchedule in dtos)
        {
            result.Add(new VC.Tenants.Application.Tenants.Models.TenantDayWorkScheduleDto(
                dayWorkSchedule.Day,
                dayWorkSchedule.StartWork,
                dayWorkSchedule.EndWork));
        }

        return result;
    }

    public static List<TenantDayWorkScheduleDto> ToApiDayWorkSchedule(this List<TenantDayWorkSchedule> tenantDayWorks)
    {
        var result = new List<TenantDayWorkScheduleDto>();

        foreach (var dayWorkSchedule in tenantDayWorks)
        {
            result.Add(new TenantDayWorkScheduleDto(
                dayWorkSchedule.Day,
                dayWorkSchedule.StartWork,
                dayWorkSchedule.EndWork));
        }

        return result;
    }
}
using Mapster;

namespace VC.Tenants.Api.Models.Response;

public record ResponseTenantWeekSchedule(IReadOnlyList<TenantDayScheduleDto> WeekDays);

internal class TenantWorkScheduleMapper : IMapsterConfig
{
    public static void Configure()
    {
        
    }
}
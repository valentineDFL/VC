using Mapster;

namespace VC.Tenants.Api.Models.Response;

public record TenantDayScheduleDto(DayOfWeek Day, DateTime StartWork, DateTime EndWork); // Transfer - Create - Update

internal class TenantDayScheduleDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        
    }
}
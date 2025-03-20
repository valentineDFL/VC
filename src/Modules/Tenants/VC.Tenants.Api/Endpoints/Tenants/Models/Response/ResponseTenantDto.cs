using VC.Tenants.Api.Endpoints.Tenants.Models;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models.Response;

public record ResponseTenantDto(
    string Name,
    string Slug,
    TenantConfigurationDto Config,
    TenantStatus Status,
    ContactInfoDto Contact,
    TenantWeekWorkScheduleDto WorkSchedule
);

internal static class ResponseTenantMappers
{
    public static ResponseTenantDto ToResponseDto(this Tenant tenant)
    {
        return new ResponseTenantDto(
            tenant.Name,
            tenant.Slug,
            tenant.Config.ToApiConfigurationDto(),
            tenant.Status,
            tenant.ContactInfo.ToApiContactDto(),
            tenant.WorkWeekSchedule.ToTenantApiWeekWorkSheduleDto()
            );
    }
}

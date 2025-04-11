using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models.Response;

public record ResponseTenantDto
    (string Name,
     string Slug,
     TenantConfigurationDto Config,
     TenantStatus Status,
     ContactInfoDto Contact,
     TenantWeekWorkScheduleDto WorkSchedule);

internal static class ResponseTenantMapper
{
    public static ResponseTenantDto ToResponseDto(this Tenant tenant)
        => new ResponseTenantDto
        (
            tenant.Name,
            tenant.Slug,
            tenant.Config.ToApiDto(),
            tenant.Status,
            tenant.ContactInfo.ToApiDto(),
            tenant.WorkWeekSchedule.ToApiDto()
        );
}

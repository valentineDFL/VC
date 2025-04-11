using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models.Request;

public record UpdateTenantRequest
    (string Name,
     string Slug,
     TenantConfigurationDto Config,
     TenantStatus Status,
     ContactInfoDto Contact,
     TenantWeekWorkScheduleDto WorkSchedule);

internal static class UpdateTenantRequestMapper 
{
    public static Application.Tenants.Models.UpdateTenantParams ToApplicationUpdateDto(this UpdateTenantRequest dto)
      => new Application.Tenants.Models.UpdateTenantParams
        (
            dto.Name,
            dto.Slug,
            dto.Config.ToApplicationDto(),
            dto.Status,
            dto.Contact.ToApplicationDto(),
            dto.WorkSchedule.ToApplicationDto()
        );
}

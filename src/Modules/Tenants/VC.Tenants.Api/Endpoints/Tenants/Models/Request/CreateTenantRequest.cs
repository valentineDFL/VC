using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models.Request;

public record CreateTenantRequest
    (string Name,
     string Slug,
     TenantConfigurationDto Config,
     TenantStatus Status,
     ContactInfoDto Contact,
     TenantWeekWorkScheduleDto WorkSchedule);

internal static class CreateTenantRequestMapper
{
    public static CreateTenantParams ToApplicationCreateDto(this CreateTenantRequest request)
        => new CreateTenantParams
        (
            request.Name,
            request.Slug, 
            request.Config.ToApplicationDto(),
            request.Status,
            request.Contact.ToApplicationDto(),
            request.WorkSchedule.ToApplicationDto()
        );
}
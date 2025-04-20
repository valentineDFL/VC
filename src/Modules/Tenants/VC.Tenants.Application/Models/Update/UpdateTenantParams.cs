using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Update;

public record UpdateTenantParams
    (string Name, 
     string Slug,
     TenantConfigurationDto Config, 
     TenantStatus Status,
     UpdateContactInfoDto ContactInfo,
     IReadOnlyList<UpdateScheduleDayDto> WeekSchedule);

internal static class UpdateTenantParamsMapper 
{
    public static Tenant ToEntity(this UpdateTenantParams dto, Guid resolvedId)
        => Tenant.Create(resolvedId, dto.Name, dto.Slug, dto.Config.ToEntity(), dto.Status, dto.ContactInfo.ToEntity(), dto.WeekSchedule.ToEntities(resolvedId));
}
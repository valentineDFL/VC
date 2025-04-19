using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Create;

public record CreateTenantParams
    (string Name,
     string Slug, 
     TenantConfigurationDto Config, 
     TenantStatus Status,
     CreateContactInfoDto ContactInfo,
     IReadOnlyList<CreateDayScheduleDto> WorkSchedule);

internal static class CreateTenantParamsMapper
{
    public static Tenant ToEntity(this CreateTenantParams dto, Guid tenantId, DateTime expiredTime)
        => Tenant.Create
        (
            tenantId,
            dto.Name, 
            dto.Slug, 
            dto.Config.ToEntity(), 
            dto.Status,
            dto.ContactInfo.ToEntity(expiredTime), 
            dto.WorkSchedule.ToEntities(tenantId)
        );
}
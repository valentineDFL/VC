using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Create;

public class CreateTenantParams
    (string name,
     string slug, 
     TenantConfigurationDto tenantConfig, 
     TenantStatus tenantStatus,
     CreateContactInfoDto contactInfo, 
     CreateWeekScheduleDto workSheduleDto)
{
    public string Name { get; } = name;

    public string Slug { get; } = slug;

    public TenantConfigurationDto TenantConfig { get; } = tenantConfig;

    public TenantStatus TenantStatus { get; } = tenantStatus;

    public CreateContactInfoDto Contact { get; } = contactInfo;

    public CreateWeekScheduleDto WorkSchedule { get; } = workSheduleDto;
}

internal static class CreateTenantParamsMapper
{
    public static Tenant ToEntity(this CreateTenantParams dto, Guid id)
        => Tenant.Create
        (
            id, 
            dto.Name, 
            dto.Slug, 
            dto.TenantConfig.ToEntity(), 
            dto.TenantStatus,
            dto.Contact.ToEntity(), 
            dto.WorkSchedule.ToEntity()
        );
}
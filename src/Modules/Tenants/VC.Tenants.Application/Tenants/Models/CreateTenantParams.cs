using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants.Models;

public class CreateTenantParams
    (string name,
     string slug, 
     TenantConfigurationDto tenantConfig, 
     TenantStatus tenantStatus,
     ContactInfoDto contactDto, 
     TenantWeekWorkSheduleDto workSheduleDto)
{
    public string Name { get; } = name;

    public string Slug { get; } = slug;

    public TenantConfigurationDto TenantConfig { get; } = tenantConfig;

    public TenantStatus TenantStatus { get; } = tenantStatus;

    public ContactInfoDto Contact { get; } = contactDto;

    public TenantWeekWorkSheduleDto WorkSchedule { get; } = workSheduleDto;
}

internal static class CreateTenantParamsMapper
{
    public static Tenant ToEntity(this CreateTenantParams createTenant)
        => Tenant.Create
        (
            Guid.CreateVersion7(),
            createTenant.Name,
            createTenant.Slug,
            createTenant.TenantConfig.ToEntity(),
            createTenant.TenantStatus,
            createTenant.Contact.ToEntity(),
            createTenant.WorkSchedule.ToEntity()
        );
}
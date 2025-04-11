using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants.Models;

public class UpdateTenantParams
    (string name, 
     string slug,
     TenantConfigurationDto tenantConfig, 
     TenantStatus tenantStatus,
     ContactInfoDto contactDto, 
     TenantWeekWorkSheduleDto worScheduleDto)
{
    public string Name { get; } = name;

    public string Slug { get; } = slug;

    public TenantConfigurationDto TenantConfig { get; } = tenantConfig;

    public TenantStatus TenantStatus { get; } = tenantStatus;

    public ContactInfoDto Contact { get; } = contactDto;

    public TenantWeekWorkSheduleDto WorkSchedule { get; } = worScheduleDto;
}

internal static class UpdateTenantParamsMapper
{
    public static Tenant ToEntity(this UpdateTenantParams updateTenant, Guid findedTenantId)
        => Tenant.Create
        (
            findedTenantId,
            updateTenant.Name,
            updateTenant.Slug,
            updateTenant.TenantConfig.ToEntity(),
            updateTenant.TenantStatus,
            updateTenant.Contact.ToEntity(),
            updateTenant.WorkSchedule.ToEntity()
        );
}

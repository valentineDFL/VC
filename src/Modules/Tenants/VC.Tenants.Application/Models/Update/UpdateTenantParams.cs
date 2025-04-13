using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Update;

public class UpdateTenantParams
    (string name, 
     string slug,
     TenantConfigurationDto tenantConfig, 
     TenantStatus tenantStatus,
     UpdateContactInfoDto contactDto,
     UpdateScheduleWeekDto weekSchedule)
{
    public string Name { get; } = name;

    public string Slug { get; } = slug;

    public TenantConfigurationDto TenantConfig { get; } = tenantConfig;

    public TenantStatus TenantStatus { get; } = tenantStatus;

    public UpdateContactInfoDto Contact { get; } = contactDto;

    public UpdateScheduleWeekDto WorkSchedule { get; } = weekSchedule;
}

internal static class UpdateTenantParamsMapper 
{
    public static Tenant ToEntity(this UpdateTenantParams dto, Guid resolvedId)
        => Tenant.Create(resolvedId, dto.Name, dto.Slug, dto.TenantConfig.ToEntity(), dto.TenantStatus, dto.Contact.ToEntity(), dto.WorkSchedule.ToEntity());
}
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants.Models;

public class CreateTenantParams(string name, string slug, 
    TenantConfigurationDto tenantConfig, TenantStatus tenantStatus,
    ContactDto contactDto, TenantWeekWorkSheduleDto workSheduleDto)
{
    public string Name { get; } = name;

    public string Slug { get; } = slug;

    public TenantConfigurationDto TenantConfig { get; } = tenantConfig;

    public TenantStatus TenantStatus { get; } = tenantStatus;

    public ContactDto Contact { get; } = contactDto;

    public TenantWeekWorkSheduleDto WorkSchedule { get; } = workSheduleDto;
}


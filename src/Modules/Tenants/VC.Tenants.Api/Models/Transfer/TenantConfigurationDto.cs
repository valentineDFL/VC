namespace VC.Tenants.Api.Models.Transfer;

public record TenantConfigurationDto
    (string About,
     string Currency,
     string Language,
     string TimeZoneId);
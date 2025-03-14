namespace VC.Tenants.Application.Tenants.Models;

public class TenantConfigurationDto(string about, string currency, string language, string timeZoneId)
{
    public string About { get; } = about;

    public string Currency { get; } = currency;

    public string Language { get; } = language;

    public string TimeZoneId { get; } = timeZoneId;
}
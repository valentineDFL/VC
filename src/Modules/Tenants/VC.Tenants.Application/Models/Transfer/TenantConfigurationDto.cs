using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Transfer;

public class TenantConfigurationDto(string about, string currency, string language, string timeZoneId)
{
    public string About { get; } = about;

    public string Currency { get; } = currency;

    public string Language { get; } = language;

    public string TimeZoneId { get; } = timeZoneId;
}

internal static class TenantConfigurationDtoMapper
{
    public static TenantConfiguration ToEntity(this TenantConfigurationDto dto)
        => TenantConfiguration.Create(dto.About, dto.Currency, dto.Language, dto.TimeZoneId);
}
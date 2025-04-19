using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Transfer;

public record TenantConfigurationDto(string About, string Currency, string Language, string TimeZoneId);

internal static class TenantConfigurationDtoMapper
{
    public static TenantConfiguration ToEntity(this TenantConfigurationDto dto)
        => TenantConfiguration.Create(dto.About, dto.Currency, dto.Language, dto.TimeZoneId);
}
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record TenantConfigurationDto
    (string About,
     string Currency,
     string Language,
     string TimeZoneId);

internal static class TenantConfigurationDtoMapper
{
    public static VC.Tenants.Application.Tenants.Models.TenantConfigurationDto ToApplicationDto(this TenantConfigurationDto dto)
        => new VC.Tenants.Application.Tenants.Models.TenantConfigurationDto(dto.About, dto.Currency, dto.Language, dto.TimeZoneId);


    public static TenantConfigurationDto ToApiDto(this TenantConfiguration config)
        => new TenantConfigurationDto(config.About, config.Currency, config.Language, config.TimeZoneId);
}
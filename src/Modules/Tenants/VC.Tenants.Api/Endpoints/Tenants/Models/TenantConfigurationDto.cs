using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record TenantConfigurationDto(
    string About,
    string Currency,
    string Language,
    /// <remarks>https://en.wikipedia.org/wiki/List_of_tz_database_time_zones</remarks>
    string TimeZoneId
    );

public static class TenantConfigurationDtoMappers
{
    public static VC.Tenants.Application.Tenants.Models.TenantConfigurationDto ToTenantConfigurationDto(this TenantConfigurationDto dto)
    {
        return new VC.Tenants.Application.Tenants.Models.TenantConfigurationDto(dto.About, dto.Currency, dto.Language, dto.TimeZoneId);
    }

    public static TenantConfigurationDto ToApiConfigurationDto(this TenantConfiguration config)
    {
        return new TenantConfigurationDto(config.About, config.Currency, config.Language, config.TimeZoneId);
    }
}
using Mapster;

namespace VC.Tenants.Api.Models.Transfer;

public record TenantConfigurationDto
    (string About,
     string Currency,
     string Language,
     string TimeZoneId);

internal class ConfigurationDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<TenantConfigurationDto, Application.Models.Transfer.TenantConfigurationDto>.NewConfig()
            .Map(dest => dest.About, src => src.About)
            .Map(dest => dest.Currency, src => src.Currency)
            .Map(dest => dest.Language, src => src.Language)
            .Map(dest => dest.TimeZoneId, src => src.TimeZoneId)
            .MapToConstructor(true);
    }
}
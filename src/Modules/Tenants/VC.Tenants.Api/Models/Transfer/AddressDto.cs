using Mapster;

namespace VC.Tenants.Api.Models.Transfer;

public record AddressDto(string Country, string City, string Street, int House);

internal class AddressDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<AddressDto, Application.Models.Transfer.AddressDto>.NewConfig()
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.House, src => src.House);
    }
}
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record AddressDto(string Country, string City, string Street, int House);

internal static class AddressDtoMapper
{
    public static VC.Tenants.Application.Tenants.Models.AddressDto ToApplicationDto(this AddressDto address)
        => new Application.Tenants.Models.AddressDto(address.Country, address.City, address.Street, address.House);

    public static AddressDto ToApiDto(this Address address)
        => new AddressDto(address.Country, address.City, address.Street, address.House);
}
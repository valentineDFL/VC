using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants.Models;

public class AddressDto(string country, string city, string street, int house)
{
    public string Country { get; } = country;

    public string City { get; } = city;

    public string Street { get; } = street;

    public int House { get; } = house;
}

internal static class AddressDtoMapper
{
    public static Address ToEntity(this AddressDto addressDto)
        => Address.Create(addressDto.Country, addressDto.City, addressDto.Street, addressDto.House);
}
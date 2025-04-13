using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Transfer;

public class AddressDto(string country, string city, string street, int house)
{
    public string Country { get; } = country;

    public string City { get; } = city;

    public string Street { get; } = street;

    public int House { get; } = house;
}

internal static class AddressDtoMapper
{
    public static Address ToEntity(this AddressDto dto)
        => Address.Create(dto.Country, dto.City, dto.Street, dto.House);
}
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Transfer;

public record AddressDto(string Country, string City, string Street, int House);

internal static class AddressDtoMapper
{
    public static Address ToEntity(this AddressDto dto)
        => Address.Create(dto.Country, dto.City, dto.Street, dto.House);
}
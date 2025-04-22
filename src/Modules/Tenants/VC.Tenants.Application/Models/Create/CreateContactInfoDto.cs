using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Create;

public record CreateContactInfoDto(string Email, string Phone, AddressDto Address);

internal static class CreateContactInfoMapper
{
    /// <summary>
    /// Метод принимает дату только в формате UTC
    /// </summary>
    public static ContactInfo ToEntity(this CreateContactInfoDto dto, DateTime expiredTime)
        => ContactInfo.Create(dto.Email, dto.Phone, dto.Address.ToEntity(), false, expiredTime);
}
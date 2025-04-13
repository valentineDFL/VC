using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Create;
public class CreateContactInfoDto(string email, string phone, AddressDto address)
{
    public string Email { get; } = email;

    public string Phone { get; } = phone;

    public AddressDto Address { get; } = address;
}

internal static class CreateContactInfoMapper
{
    public static ContactInfo ToEntity(this CreateContactInfoDto dto)
        => ContactInfo.Create(dto.Email, dto.Phone, dto.Address.ToEntity(), false, DateTime.UtcNow.AddMinutes(ContactInfo.LinkMinuteValidTime));
}

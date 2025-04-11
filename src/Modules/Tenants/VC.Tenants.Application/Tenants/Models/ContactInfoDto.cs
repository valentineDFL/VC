using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants.Models;

public class ContactInfoDto(string email, string phone, AddressDto address)
{
    public string Email { get; } = email;

    public string Phone { get; } = phone;
    
    public AddressDto Address { get; } = address;
}

internal static class ContactInfoDtoMapper
{
    public static ContactInfo ToEntity(this ContactInfoDto contactInfoDto)
        => ContactInfo.Create
        (
            contactInfoDto.Email, 
            contactInfoDto.Phone, 
            contactInfoDto.Address.ToEntity(), 
            false,
            DateTime.UtcNow.AddMinutes(ContactInfo.LinkMinuteValidTime)
        );
}
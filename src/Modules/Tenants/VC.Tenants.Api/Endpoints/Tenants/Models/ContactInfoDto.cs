using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record ContactInfoDto(string Email, string Phone, AddressDto Address);

internal static class ContactDtoMapper
{
    public static Application.Tenants.Models.ContactInfoDto ToApplicationDto(this ContactInfoDto dto)
        => new Application.Tenants.Models.ContactInfoDto(dto.Email, dto.Phone, dto.Address.ToApplicationDto());

    public static ContactInfoDto ToApiDto(this ContactInfo contactInfo)
        => new ContactInfoDto(contactInfo.Email, contactInfo.Phone, contactInfo.Address.ToApiDto());
}
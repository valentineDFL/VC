using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record ContactInfoDto(string Email, string Phone, string Address);

public static class ContactDtoMappers
{
    public static Application.Tenants.Models.ContactDto ToApplicationContactDto(this ContactInfoDto dto)
    {
        return new Application.Tenants.Models.ContactDto(dto.Email, dto.Phone, dto.Address);
    }

    public static ContactInfoDto ToApiContactDto(this ContactInfo contactInfo)
    {
        return new ContactInfoDto(contactInfo.Email, contactInfo.Phone, contactInfo.Address);
    }
}
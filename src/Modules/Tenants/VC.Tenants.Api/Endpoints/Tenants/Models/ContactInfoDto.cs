namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record ContactInfoDto(string Email, string Phone, string Address);

public static class ContactDtoMappers
{
    public static Application.Tenants.Models.ContactDto ToContactDto(this ContactInfoDto dto)
    {
        return new Application.Tenants.Models.ContactDto(dto.Email, dto.Phone, dto.Address);
    }
}
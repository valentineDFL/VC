using VC.Tenants.Application.Tenants.Models;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record CreateTenantRequest(string Name, string Slug, string TimeZoneId, ContactDto Contact);

public static class CreateTenantRequestMappers
{
    public static CreateTenantParams ToCreateTenantParams(this CreateTenantRequest request)
    {
        return new CreateTenantParams(request.Name, request.Slug, request.TimeZoneId, request.Contact.ToContactDto());
    }
}
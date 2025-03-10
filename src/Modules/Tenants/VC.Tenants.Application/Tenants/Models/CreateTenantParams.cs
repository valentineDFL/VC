namespace VC.Tenants.Application.Tenants.Models;

public class CreateTenantParams(string name, string slug, string timeZoneId, ContactDto contact)
{
    public string Name { get; } = name;
    public string Slug { get; } = slug;
    public string TimeZoneId { get; } = timeZoneId;
    public ContactDto Contact { get; } = contact;
}
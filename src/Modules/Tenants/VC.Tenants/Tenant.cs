namespace VC.Tenants;

/// <summary>
/// Арендатор.
/// </summary>
internal class Tenant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    /// <summary>
    /// Уникальный URL-идентификатор.
    /// </summary>
    public string Slug { get; set; }
    
    /// <remarks>https://en.wikipedia.org/wiki/List_of_tz_database_time_zones</remarks>
    public string TimeZoneId { get; set; }

    public TenantStatus Status { get; set; }

    public ContactInfo ContactInfo { get; set; }
}
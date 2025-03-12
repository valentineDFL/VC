namespace VC.Tenants.Models;

/// <summary>
/// Арендатор.
/// </summary>
public class Tenant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    /// <summary>
    /// Уникальный URL-идентификатор.
    /// </summary>
    public string Slug { get; set; }
    
    /// <remarks>https://en.wikipedia.org/wiki/List_of_tz_database_time_zones</remarks>
    public TenantConfiguration Config { get; set; }

    // public string CompanyLogoUrl { get; set; } // В будущем разобраться с тем как локально разместить
    // лого и получать его на фронте


    public TenantStatus Status { get; set; }

    public ContactInfo ContactInfo { get; set; }

    public TenantWorkSchedule WorkWeekSchedule { get; set; }
}
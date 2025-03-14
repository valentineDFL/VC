namespace VC.Tenants.Entities;

public class TenantConfiguration
{
    public string About { get; set; }

    public string Currency { get; set; }

    public string Language { get; set; }

    /// <remarks>https://en.wikipedia.org/wiki/List_of_tz_database_time_zones</remarks>
    public string TimeZoneId { get; set; }
}
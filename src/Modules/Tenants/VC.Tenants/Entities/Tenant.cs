namespace VC.Tenants.Entities;

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
    
    public TenantConfiguration Config { get; set; }

    public TenantStatus Status { get; set; }

    public ContactInfo ContactInfo { get; set; }

    public TenantWorkSchedule WorkWeekSchedule { get; set; }
}
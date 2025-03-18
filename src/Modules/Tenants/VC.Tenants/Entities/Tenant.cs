using System.ComponentModel;

namespace VC.Tenants.Entities;

/// <summary>
/// Арендатор.
/// </summary>
public class Tenant
{
    public Guid Id { get; set; }

    [DefaultValue("Test")]
    public string Name { get; set; }
    
    /// <summary>
    /// Уникальный URL-идентификатор.
    /// </summary>
    public string Slug { get; set; }
    
    public TenantConfiguration Config { get; set; }

    [DefaultValue(1)]
    public TenantStatus Status { get; set; }

    public ContactInfo ContactInfo { get; set; }

    public TenantWorkSchedule WorkWeekSchedule { get; set; }
}
namespace VC.Tenants.Entities;

/// <summary>
/// Арендатор.
/// </summary>
public class Tenant
{
    public const int NameMinLenght = 3;
    public const int NameMaxLenght = 32;

    public const int SlugMinLength = 10;
    public const int SlugMaxLength = 128;

    private Tenant(Guid id, string name, string slug, TenantConfiguration config, TenantStatus status, ContactInfo contactInfo, TenantWeekSchedule workWeekSchedule)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Config = config;
        Status = status;
        ContactInfo = contactInfo;
        WorkWeekSchedule = workWeekSchedule;
    }

    private Tenant() { }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    /// <summary>
    /// Уникальный URL-идентификатор.
    /// </summary>
    public string Slug { get; private set; }
    
    public TenantConfiguration Config { get; private set; }

    public TenantStatus Status { get; private set; }
    
    public ContactInfo ContactInfo { get; private set; }

    public TenantWeekSchedule WorkWeekSchedule { get; private set; }

    public static Tenant Create(Guid id, string name, string slug, TenantConfiguration config, TenantStatus status, ContactInfo contactInfo, TenantWeekSchedule workWeekSchedule)
    {
        if(id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty");

        if(name.Length > NameMaxLenght || name.Length < NameMinLenght)
            throw new ArgumentException($"Name length must be greater that {NameMinLenght} or equals. Lowest than {NameMaxLenght} or equals.");

        if (slug.Length > SlugMaxLength || slug.Length < SlugMinLength)
            throw new ArgumentException($"Slug length must be greater than {SlugMinLength - 1} or equals. Lowest than {SlugMaxLength} or equals.");

        if(config is null)
            throw new ArgumentNullException("Config cannot be null");

        if (contactInfo is null)
            throw new ArgumentNullException("Contact Info cannot be null");

        if (workWeekSchedule is null)
            throw new ArgumentNullException("WorkWeekSchedule cannot be null");

        return new Tenant(id, name, slug, config, status, contactInfo, workWeekSchedule);
    }
}
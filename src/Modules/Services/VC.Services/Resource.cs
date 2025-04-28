using VC.Services.Common;

namespace VC.Services;

/// <summary>
/// Ресурс необходимый для выполнения услуги.
/// </summary>
public class Resource : AggregateRoot<Guid>, IHasTenantId
{
    public Resource(Guid id, Guid tenantId) : base(id)
    {
        Id = id;
        TenantId = tenantId;
    }

    public Guid TenantId { get; private set; }
    public string Title { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Количество ресурса.
    /// </summary>
    public int Count { get; set; }
}
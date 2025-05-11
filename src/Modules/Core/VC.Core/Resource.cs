using VC.Core.Common;

namespace VC.Core;

/// <summary>
/// Ресурс необходимый для выполнения услуги.
/// </summary>
public class Resource : AggregateRoot<Guid>, IHasTenantId
{
    public Resource(Guid id, Guid tenantId, string title, int count) : base(id)
    {
        TenantId = tenantId;
        Title = title;
        Count = count;
    }

    public Guid TenantId { get; private set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// Количество ресурса.
    /// </summary>
    public int Count { get; set; }
}
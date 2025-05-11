using VC.Core.Common;

namespace VC.Core.Services;

public class Category : AggregateRoot<Guid>, IHasTenantId
{
    public Category(Guid id, string title, Guid tenantId) : base(id)
    {
        Title = title;
        TenantId = tenantId;
    }
    
    public string Title { get; set; }
    public Category? ParentCategory { get; set; }
    public Guid TenantId { get; private set; }
}

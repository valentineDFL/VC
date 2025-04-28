using VC.Services.Common;

namespace VC.Services;

public class Category : AggregateRoot<Guid>, IHasTenantId
{
    public Category(Guid id, string title, Category? parentCategory, Guid tenantId) : base(id)
    {
        Title = title;
        ParentCategory = parentCategory;
        TenantId = tenantId;
    }

    public string Title { get; }
    public Category? ParentCategory { get; }
    public Guid TenantId { get; }
}

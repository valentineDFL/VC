namespace VC.Recources.Resource.Domain.Entities;

public class Resource
{
    public Resource(Guid tenantId, string name, ResourceType resourceType)
    {
        Id = Guid.CreateVersion7();
        TenantId = tenantId;
        Name = name;
        ResourceType = resourceType;
    }

    /// <summary>
    /// поменять глобальное название
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Resource id
    /// </summary>
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; } 

    public ResourceType ResourceType { get; set; }

    public bool IsActive { get; set; }

    public Dictionary<string, object> Attributes { get; set; }

    public void Deactivate()
    {
        IsActive = false;
    }
}

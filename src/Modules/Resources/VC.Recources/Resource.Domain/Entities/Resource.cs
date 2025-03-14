namespace VC.Recources.Resource.Domain.Entities;

public class Resource
{
    /// <summary>
    /// поменять глобальное название
    /// </summary>
    public Guid TetnantId { get; set; }
    
    /// <summary>
    /// Resource id
    /// </summary>
    public Guid Id { get; set; }
    
    public string Name { get; set; }    
    
    public ResourceType ResourceType { get; set; }
    
    public bool IsActive { get; set; }

    private List<ValueObject.Attribute> _attributes = new List<ValueObject.Attribute>();

    public IReadOnlyCollection<ValueObject.Attribute> Attributes => _attributes.AsReadOnly();

    public Resource(Guid tenantId, string name, ResourceType resourceType)
    {
        Id = Guid.CreateVersion7();
        TetnantId = tenantId;
        Name = name;
        ResourceType = resourceType;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}

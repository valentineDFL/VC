using VC.Recources.Resource.Domain.ValueObject;

namespace VC.Recources.Resource.Domain;

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

    private List<ResourceAttribute> _attributes = new List<ResourceAttribute>();

    public IReadOnlyCollection<ResourceAttribute> Attributes => _attributes.AsReadOnly();

    public Resource(Guid tenantId, string name, ResourceType resourceType)
    {
        Id = Guid.CreateVersion7();
        TetnantId = tenantId;
        Name = name;
        ResourceType = resourceType;
    }

    public void AddAttribute(string key,object value)
    {
        _attributes.Add(new ResourceAttribute(key,value));
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}

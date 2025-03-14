using VC.Recources.Resource.Domain.ValueObject;

namespace VC.Recources.Resource.Domain;

public class ResourceType
{
    public string Name { get; set; }

    public IReadOnlyCollection<ResourceAttributeDefinition> ResourceAttributeDefinitions => _resourceAttributeDefinitions.AsReadOnly();
    private List<ResourceAttributeDefinition> _resourceAttributeDefinitions = new List<ResourceAttributeDefinition>();

    public ResourceType(string name)
    {
        Name = name;
    }

    public void AddAttributeDefinition(string key, string type, bool isRequired = false)
    {
        _resourceAttributeDefinitions.Add(new ResourceAttributeDefinition(key,type,isRequired))
    }
}

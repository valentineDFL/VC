using VC.Recources.Resource.Domain.Exceptions;
using VC.Recources.Resource.Domain.ValueObject;

namespace VC.Recources.Resource.Domain.Entities;

public class ResourceType
{
    public string Name { get; set; }

    public IReadOnlyCollection<AttributeDefinition> ResourceAttributeDefinitions => _resourceAttributeDefinitions.AsReadOnly();

    private List<AttributeDefinition> _resourceAttributeDefinitions = new List<AttributeDefinition>();

    public ResourceType(string name)
    {
        Name = name;
    }

    public void AddAttributeDefinition(AttributeDefinition resourceAttributeDefinitions)
    {
        if (_resourceAttributeDefinitions.Any(d => d.Key == resourceAttributeDefinitions.Key))
        {
            throw new DomainException($"Атрибут {resourceAttributeDefinitions.Key} уже существует.");
        }

        _resourceAttributeDefinitions.Add(resourceAttributeDefinitions);
    }
}

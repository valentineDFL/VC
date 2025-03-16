using VC.Recources.Resource.Domain.Exceptions;
using VC.Recources.Resource.Domain.ValueObject;

namespace VC.Recources.Resource.Domain.Entities;

public class ResourceType
{
    public string Name { get; set; }

    public IReadOnlyCollection<AttributeDefinition> AttributeDefinitions => _attributeDefinitions.AsReadOnly();

    private List<AttributeDefinition> _attributeDefinitions = new List<AttributeDefinition>();

    public ResourceType(string name)
    {
        Name = name;
    }

    public void AddAttributeDefinition(AttributeDefinition attributeDefinitions)
    {
        if (_attributeDefinitions.Any(d => d.Key == attributeDefinitions.Key))
        {
            throw new DomainException($"Атрибут {attributeDefinitions.Key} уже существует.");
        }

        _attributeDefinitions.Add(attributeDefinitions);
    }
}

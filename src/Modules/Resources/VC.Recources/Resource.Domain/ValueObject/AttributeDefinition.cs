using VC.Recources.Resource.Domain.Enums;
using VC.Recources.Resource.Domain.Exceptions;

namespace VC.Recources.Resource.Domain.ValueObject;

public class AttributeDefinition
{
    public string Key { get; set; }
    
    public AttributeType AttributeType { get; set; }

    public bool IsRequired { get; set; }

    private AttributeDefinition(
        string key,
        AttributeType attributeType,
        bool isRequired
        )
    {
        Key = key;
        AttributeType = attributeType;
        IsRequired = isRequired;
    }

    public static AttributeDefinition CreateAttributeDefinition(
        string key,
        AttributeType attributeType,
        bool isRequired = false)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new DomainException("Ключ не может быть пустым.");

        return new AttributeDefinition(key, attributeType, isRequired);
    }
}

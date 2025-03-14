using VC.Recources.Resource.Domain.Enums;
using VC.Recources.Resource.Domain.Exceptions;

namespace VC.Recources.Resource.Domain.ValueObject;

public class Attribute
{
    public string Key { get; set; }

    public object Value { get; set; }

    public AttributeType AttributeType { get; set; }

    private static readonly Dictionary<AttributeType, Func<object, bool>> Validators =
        new()
        {
            { AttributeType.String, v => v is string },
            { AttributeType.Number, v => v is int or double },
            { AttributeType.Booolean, v => v is bool }
        };

    public Attribute(
        string key,
        object value,
        AttributeType attributeType
        )
    {
        Key = key;
        Value = value;
        AttributeType = attributeType;
    }

    private static bool IsValueValid(object value, AttributeType attributeType)
    {
        return Validators.TryGetValue(attributeType, out var isValid) && isValid(value);
    }

    public static Attribute CreateAttribute(string key, object value, AttributeType attributeType)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new DomainException("Ключ атрибута не может быть пустым.");
        }

        if (!IsValueValid(value, attributeType))
        {
            throw new DomainException($"Некорректное значение для типа {attributeType}.");
        }

        return new Attribute(key, value, attributeType);
    }
}

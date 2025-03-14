namespace VC.Recources.Resource.Domain.ValueObject;

public class ResourceAttributeDefinition
{
    public string Key { get; set; }

    public string Type { get; set; }

    public bool IsRequired { get; set; }

    public ResourceAttributeDefinition(string key, string type, bool isRequired)
    {
        Key = key;
        Type = type;
        IsRequired = isRequired;
    }
}

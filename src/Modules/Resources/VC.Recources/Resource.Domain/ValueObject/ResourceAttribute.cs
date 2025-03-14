namespace VC.Recources.Resource.Domain.ValueObject;

public  class ResourceAttribute
{
    public string Key { get; set; }
    
    public object Value { get; set; }

    public ResourceAttribute(string key,object value)
    {
        Key = key;
        Value = value;
    }
}

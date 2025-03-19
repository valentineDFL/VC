using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace VC.Utilities;

public class OpenApiDefaultValuesConfigurator : IOpenApiSchemaTransformer
{
    private Dictionary<string, IOpenApiAny> _initializedKeys = new Dictionary<string, IOpenApiAny>();

    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        int i = 0;
        foreach (var property in schema.Properties)
        {
            var type = context.JsonTypeInfo.Properties[i].PropertyType;

            SetDefaultValueForType(type, schema, property.Key);

            i++;
        }
        return Task.CompletedTask;
    }

    private void SetDefaultValueForType(Type type, OpenApiSchema schema, string propertyKey)
    {
        var property = schema.Properties[propertyKey];

        if (_initializedKeys.ContainsKey(propertyKey))
        {
            SetDefaultValueFromDefaultValueAtribute(property, propertyKey, type);
            return;
        }

        if (property.Default is not null && !_initializedKeys.ContainsKey(propertyKey))
        {
            _initializedKeys.Add(propertyKey, property.Default);
            return;
        }

        SetDefaultTypesValue(type, property);
    }

    private void SetDefaultValueFromDefaultValueAtribute(OpenApiSchema property, string propertyKey, Type type)
    {
        var value = _initializedKeys[propertyKey];

        var stringValue = value as OpenApiString;
        if (stringValue is not null)
        {
            SetStringValue(property, type, stringValue.Value);
            return;
        }

        var intValue = value as OpenApiInteger;
        if (intValue is not null)
        {
            property.Default = new OpenApiInteger(intValue.Value);
            return;
        }

        var boolValue = value as OpenApiBoolean;
        if(boolValue is not null)
        {
            property.Default = new OpenApiBoolean(boolValue.Value);
            return;
        }
    }

    private static void SetDefaultTypesValue(Type type, OpenApiSchema property)
    {
        if (type == typeof(Guid))
            property.Default = new OpenApiString(Guid.Empty.ToString());

        else if (type == typeof(DateTime))
            property.Default = new OpenApiString(DateTime.UtcNow.ToString());

        else if (type == typeof(int))
            property.Default = new OpenApiInteger(0);

        else if (type == typeof(bool))
            property.Default = new OpenApiBoolean(false);

        else if (type == typeof(string))
            property.Default = new OpenApiString("string");
    }

    private static void SetStringValue(OpenApiSchema property, Type type, string value)
    {
        if (type == typeof(Guid))
            property.Default = new OpenApiString(value);

        else if (type == typeof(DateTime))
            property.Default = new OpenApiString(value);

        else if (type == typeof(string))
            property.Default = new OpenApiString(value);
    }
}
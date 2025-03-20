using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace VC.Utilities;

public class OpenApiDefaultValuesConfigurator : IOpenApiSchemaTransformer
{
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

        if (property.Default is not null)
            return;

        SetDefaultPropertyValue(type, property);
    }

    private static void SetDefaultPropertyValue(Type type, OpenApiSchema property)
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
}
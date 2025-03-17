using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace VC.Utilities;

public class OpenApiDefaultValuesConfigurator
{
    /// <summary>
    /// Конфигурирует в OpenApi Json значения по умолчанию (Как Сваггер)
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    public static void SetDefaultValuesForTypes(OpenApiSchema schema, OpenApiSchemaTransformerContext context)
    {
        int i = 0;
        foreach (var property in schema.Properties)
        {
            var type = context.JsonTypeInfo.Properties[i].PropertyType;

            SetDefaultValueForType(type, schema, property.Key);
            i++;
        }
    }

    private static void SetDefaultValueForType(Type type, OpenApiSchema schema, string propertyKey)
    {
        if (type == typeof(Guid))
            schema.Properties[propertyKey].Default = new OpenApiString(Guid.Empty.ToString());

        else if (type == typeof(DateTime))
            schema.Properties[propertyKey].Default = new OpenApiString(DateTime.UtcNow.ToString());

        else if (type == typeof(int))
            schema.Properties[propertyKey].Default = new OpenApiInteger(0);

        else if (type == typeof(bool))
            schema.Properties[propertyKey].Default = new OpenApiString("false");

        else 
            schema.Properties[propertyKey].Default = new OpenApiString("string");
    }
}

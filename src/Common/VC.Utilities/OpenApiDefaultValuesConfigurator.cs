using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace VC.Utilities;

public class OpenApiDefaultValuesConfigurator : IOpenApiSchemaTransformer
{
    /// <summary>
    /// Конфигурирует в OpenApi Json значения по умолчанию (Как Сваггер)
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
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

        Console.WriteLine(schema.Properties[propertyKey].Default == null);

        if (type == typeof(Guid))
            property.Default = new OpenApiString(Guid.Empty.ToString());

        else if (type == typeof(DateTime))
            property.Default = new OpenApiString(DateTime.UtcNow.ToString());

        else if (type == typeof(int))
            property.Default = new OpenApiInteger(0);

        else if (type == typeof(bool))
            property.Default = new OpenApiString(false.ToString());

        else if (type == typeof(string))
        {
            if(property.Default == null)
               property.Default = new OpenApiString("string");

            Console.WriteLine(property.Default == null);

            //if(propertyVal == null)
            //{
            //    Console.WriteLine("Bad");
            //    property.Default = new OpenApiString("string");
            //    return;
            //}

            // property.Default = new OpenApiString(propertyVal.Value.ToString());

            //property.Default = new OpenApiString("string");

            //Console.WriteLine($"{propertyVal.Value}");
        }
    }

    private void ReWritePropertyDefaultValue()
    {

    }
}
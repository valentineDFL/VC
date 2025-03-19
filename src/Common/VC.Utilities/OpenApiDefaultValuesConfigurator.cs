using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace VC.Utilities;

public class OpenApiDefaultValuesConfigurator : IOpenApiSchemaTransformer
{
    /// <summary>
    /// Хранит состояние полей которые имею Data Annotations значение по умолчанию
    /// </summary>
    private Dictionary<string, IOpenApiAny> _initializedKeys = new Dictionary<string, IOpenApiAny>();

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

        if (property.Default is not null && !_initializedKeys.ContainsKey(propertyKey))
        {
            _initializedKeys.Add(propertyKey, property.Default);
            return;
        }
        else if (_initializedKeys.ContainsKey(propertyKey))
        {
            SetDefaultValueFromDataAnnotation(property, propertyKey, type);
            return;
        }

        SetDefaultValueLikeSwagger(type, property);
    }

    /// <summary>
    /// Устанавливает значение которое зарезервировано атрибутом [DefaultValue()]
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyKey"></param>
    /// <param name="type"></param>
    private void SetDefaultValueFromDataAnnotation(OpenApiSchema property, string propertyKey, Type type)
    {
        var value = _initializedKeys[propertyKey];

        var stringValue = value as OpenApiString;
        if (stringValue is not null)
        {
            SetValue(property, type, stringValue.Value);

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

    /// <summary>
    /// Если в Data Annotations атрибуте не стоит значение, то этот устанавливает своё значение по умолчанию
    /// </summary>
    /// <param name="type"></param>
    /// <param name="property"></param>
    private static void SetDefaultValueLikeSwagger(Type type, OpenApiSchema property)
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

    private static void SetValue(OpenApiSchema property, Type type, string value)
    {
        if (type == typeof(Guid))
            property.Default = new OpenApiString(value);

        else if (type == typeof(DateTime))
            property.Default = new OpenApiString(value);

        else if (type == typeof(string))
            property.Default = new OpenApiString(value);
    }
}
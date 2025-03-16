using Microsoft.AspNetCore.OpenApi;

namespace VC.Tenants.Api.OpenApi;

public class OpenApiConfig
{
    public const string GroupName = "Tenants Module";
    public const string DocumentName = "tenants";
    
    public static OpenApiOptions ConfigureOpenApi(OpenApiOptions opts)
    {
        opts.ShouldInclude = description => description.GroupName == GroupName;
        opts.AddDocumentTransformer((document, ctx, ctl) =>
            {
                document.Info = new()
                {
                    Version = "v1",
                    Title = "Управление арендаторами"
                };

                return Task.CompletedTask;
            }
        );

        opts.AddSchemaTransformer((schema, ctx, ctk) =>
        {
            foreach (var property in schema.Properties)
            {
                var propertySchema = property.Value;

                if (propertySchema.Type == typeof(int).Name.ToLower())
                    propertySchema.Default = new Microsoft.OpenApi.Any.OpenApiInteger(0);

                if (propertySchema.Type == typeof(string).Name.ToLower())
                    propertySchema.Default = new Microsoft.OpenApi.Any.OpenApiString("string");

                if (propertySchema.Type == typeof(DateTime).Name.ToLower())
                    propertySchema.Default = new Microsoft.OpenApi.Any.OpenApiDateTime(DateTime.UtcNow);
            }

            return Task.CompletedTask;
        });

        return opts;
    }
}

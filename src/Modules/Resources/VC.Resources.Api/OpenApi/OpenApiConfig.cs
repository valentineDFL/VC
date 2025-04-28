using Microsoft.AspNetCore.OpenApi;
using VC.Utilities;

namespace VC.Resources.Api.OpenApi;

public class OpenApiConfig
{
    public const string GroupName = "Resources Module";
    public const string DocumentName = "resources";

    public static OpenApiOptions ConfigureOpenApi(OpenApiOptions opts)
    {
        opts.ShouldInclude = description => description.GroupName == GroupName;
        opts.AddDocumentTransformer((document, ctx, ctl) =>
        {
            document.Info = new()
            {
                Version = "v1",
                Title = "Управление ресурсами"
            };

            return Task.CompletedTask;
        });
        opts.AddSchemaTransformer<OpenApiDefaultValuesConfigurator>();

        return opts;
    }
}

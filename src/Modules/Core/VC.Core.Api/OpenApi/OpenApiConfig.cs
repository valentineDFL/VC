using Microsoft.AspNetCore.OpenApi;
using VC.Utilities;

namespace VC.Core.Api.OpenApi;

public class OpenApiConfig
{
    public const string GroupName = "Services Module";
    public const string DocumentName = "services";

    public static OpenApiOptions ConfigureOpenApi(OpenApiOptions opts)
    {
        opts.ShouldInclude = description => description.GroupName == GroupName;
        opts.AddDocumentTransformer((document, ctx, ctl) =>
        {
            document.Info = new()
            {
                Version = "v1",
                Title = "Управление услугами"
            };

            return Task.CompletedTask;
        });
        opts.AddSchemaTransformer<OpenApiDefaultValuesConfigurator>();

        return opts;
    }
}

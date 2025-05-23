using Microsoft.AspNetCore.OpenApi;
using VC.Shared.Utilities;

namespace VC.Auth.Api.OpenApi;

public class OpenApiConfig
{
    public const string GroupName = "Auth Module";
    public const string DocumentName = "auth";

    public static OpenApiOptions ConfigureOpenApi(OpenApiOptions opts)
    {
        opts.ShouldInclude = description => description.GroupName == GroupName;
        opts.AddDocumentTransformer((document, ctx, ctl) =>
        {
            document.Info = new()
            {
                Version = "v1",
                Title = "Регистрация"
            };

            return Task.CompletedTask;
        });
        opts.AddSchemaTransformer<OpenApiDefaultValuesConfigurator>();

        return opts;
    }
}
using Microsoft.AspNetCore.OpenApi;

namespace VC.Orders.Api.OpenApi;

public class OpenApiConfig
{
    public const string GroupName = "Orders Module";
    public const string DocumentName = "orders";
    
    public static OpenApiOptions ConfigureOpenApi(OpenApiOptions opts)
    {
        opts.ShouldInclude = description => description.GroupName == GroupName;
        opts.AddDocumentTransformer((document, ctx, ctl) =>
            {
                document.Info = new()
                {
                    Version = "v1",
                    Title = "Управление заказами"
                };
            
                return Task.CompletedTask;
            }
        );

        return opts;
    }
}

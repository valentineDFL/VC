using Microsoft.AspNetCore.OpenApi;

namespace VC.Orders.Api.OpenApi;

public class OpenApiConfig
{
    public const string GroupName = "Bookings Module";
    public const string DocumentName = "bookings";
    
    public static OpenApiOptions ConfigureOpenApi(OpenApiOptions opts)
    {
        opts.ShouldInclude = description => description.GroupName == GroupName;
        opts.AddDocumentTransformer((document, ctx, ctl) =>
            {
                document.Info = new()
                {
                    Version = "v1",
                    Title = "Управление бронированиями"
                };
            
                return Task.CompletedTask;
            }
        );

        return opts;
    }
}

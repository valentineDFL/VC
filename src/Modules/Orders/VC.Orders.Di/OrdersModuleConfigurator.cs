using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Orders.Api.Endpoints.Orders;
using VC.Orders.Api.OpenApi;

namespace VC.Orders.Di;

public static class OrdersModuleConfigurator
{
    public static IServiceCollection ConfigureOrdersModule(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationConfiguration.Configure(services, configuration);
        return services
            .ConfigureOpenApi(configuration);
    }

    public static IServiceCollection ConfigureOpenApi(this IServiceCollection services, IConfiguration configuration)
        => services.AddOpenApi(OpenApiConfig.DocumentName, opts => OpenApiConfig.ConfigureOpenApi(opts));
    
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        =>  builder.AddBookingsEndpoints();
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Application.UseCases.Orders;
using VC.Shared.Utilities.Options.Uris;

namespace VC.Orders.Di;

public static class ApplicationConfiguration
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IGetOrderUseCase, GetOrderUseCase>();
        services.AddScoped<ICancelOrderUseCase, CancelOrderUseCase>();

        services.Configure<CoreModuleControllers>(configuration.GetSection(nameof(CoreModuleControllers)));
    }
}
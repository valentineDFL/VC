using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VC.Orders.Infrastructure.Persistence;
using VC.Orders.Repositories;
using VC.Orders.Infrastructure.Persistence.Repositories;

namespace VC.Orders.Di;

public static class InfrastructureConfiguration
{
    public static void ConfigureInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSql");

        serviceCollection.AddDbContext<OrdersDbContext>(options => options
        .UseNpgsql(connectionString, x =>
        {
            x.MigrationsHistoryTable("__EFMigrationsHistory", OrdersDbContext.Schema);
        }));

        ConfigureRepositories(serviceCollection);
    }

    private static void ConfigureRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IOrdersRepository, OrdersRepository>();
        serviceCollection.AddScoped<IPaymentsRepository, PaymentsRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
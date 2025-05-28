using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VC.Orders.Infrastructure.Persistence;
using VC.Orders.Repositories;
using VC.Orders.Infrastructure.Persistence.Repositories;
using VC.Orders.Application;
using VC.Orders.Infrastructure.Implementations;
using VC.Shared.Utilities.Options;

namespace VC.Orders.Di;

public static class InfrastructureConfiguration
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings))
            .Get<ConnectionStrings>();

        services.AddDbContext<OrdersDbContext>(options => options
        .UseNpgsql(connectionStrings.PostgresSQL, x =>
        {
            x.MigrationsHistoryTable("__EFMigrationsHistory", OrdersDbContext.Schema);
        }));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionStrings.Redis;
            options.InstanceName = "cacheSalt:";
        });

        ConfigureRepositories(services);
        ConfigureImplementations(services);
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IPaymentsRepository, PaymentsRepository>();

        services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();

        services.AddKeyedScoped<IOrdersIdempotenciesRepository, PostgresOrdersIdempotenciesRepository>(IUnitOfWork.PostgresOrdersKey);
        services.AddKeyedScoped<IOrdersIdempotenciesRepository, RedisCacheOrdersIdempotenciesRepository>(IUnitOfWork.RedisCacheOrdersKey);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void ConfigureImplementations(IServiceCollection services)
    {
        services.AddSingleton<IIdempodencyKeyGenerator, GuidIdempodencyKeyGenerator>();
    }
}
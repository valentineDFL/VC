using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Core.Application.Events;
using VC.Core.Application.ServicesUseCases;
using VC.Core.Repositories;
using VC.Core.Infrastructure.Implementations;
using VC.Core.Infrastructure.Persistence;
using VC.Core.Infrastructure.Persistence.Queries;
using VC.Core.Infrastructure.Persistence.Repositories;
using VC.Shared.RabbitMQIntegration.Consumers.Interfaces;
using VC.Core.Infrastructure.Implementations.RabbitConsumers;
using VC.Core.Infrastructure.BackgroundServices;
using RabbitMQ.Client.Events;

namespace VC.Core.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureCoreInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        ConfigureBrokers(services);

        services
            .ConfigureEvents()
            .ConfigurePersistence(configuration);
    }

    public static IServiceCollection ConfigureEvents(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, InMemoryDomainEventDispatcher>();
        
        return services;
    }

    public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSql");

        services.AddDbContext<DatabaseContext>(options => options
            .UseNpgsql(
                connectionString,
                x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseContext.Schema);
                    x.ConfigureDataSource(b => b.EnableDynamicJson());
                }
            ));
        
        services.AddScoped<IResourcesRepository, ResourcesRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<IWorkSchedulesRepository, WorkSchedulesRepository>();

        services.AddScoped<IOrdersHistoryRepository, OrdersHistoryRepository>();

        services.AddScoped<IGetServiceDetailsUseCase, GetServiceDetailsUseCase>();
        services.AddScoped<IGetAllServicesUseCase, GetAllServicesUseCase>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<RabbitConsumersBackgroundService>();

        return services;
    }

    private static void ConfigureBrokers(IServiceCollection services)
    {
        services.AddSingleton<IConsumer, CreatedOrdersConsumer>();
        services.AddSingleton<IConsumer, OrdersWithChangedStateConsumer>();
    }
}
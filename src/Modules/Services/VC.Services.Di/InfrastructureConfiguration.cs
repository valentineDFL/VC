using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Services.Application.Events;
using VC.Services.Application.ServicesUseCases;
using VC.Services.Infrastructure.Implementations;
using VC.Services.Infrastructure.Persistence;
using VC.Services.Infrastructure.Persistence.Queries;
using VC.Services.Infrastructure.Persistence.Repositories;
using VC.Services.Repositories;

namespace VC.Services.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureServicesInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
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
                x => { x.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseContext.Schema); }
            ));
        
        services.AddScoped<IResourcesRepository, ResourcesRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        
        services.AddScoped<IGetServiceDetailsUseCase, GetServiceDetailsUseCase>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
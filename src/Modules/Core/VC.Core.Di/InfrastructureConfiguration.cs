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

namespace VC.Core.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureCoreInfrastructure(this IServiceCollection services,
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
        
        services.AddScoped<IGetServiceDetailsUseCase, GetServiceDetailsUseCase>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
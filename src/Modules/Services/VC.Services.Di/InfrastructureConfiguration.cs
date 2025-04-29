using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Services.Application.ServicesUseCases;
using VC.Services.Infrastructure.Persistence;
using VC.Services.Infrastructure.Persistence.Queries;
using VC.Services.Infrastructure.Persistence.Repositories;
using VC.Services.Repositories;

namespace VC.Services.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureServicesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<DatabaseContext>(options => options
            .UseNpgsql(
                connectionString,
                x => { x.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseContext.Schema); }
            ));

        ConfigureInfrastructure(services);
    }

    private static void ConfigureInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IResourcesRepository, ResourcesRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        
        
        services.AddScoped<IServiceDetailsQuery, ServiceDetailsQuery>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
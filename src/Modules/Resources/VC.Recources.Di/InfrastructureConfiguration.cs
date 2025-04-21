using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VC.Recources.Domain;
using VC.Recources.Infrastructure;
using VC.Recources.Infrastructure.Repositories;
using VC.Utilities;
using ResourceDbContext = VC.Recources.Infrastructure.ResourceDbContext;

namespace VC.Recources.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureResourcesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<ResourceDbContext>(options => options
            .UseNpgsql(
                connectionString,
                x => { x.MigrationsHistoryTable("__EFMigrationsHistory", ResourceDbContext.ResourcesSchema); }
            ));

        ConfigureInfrastructure(services);
    }

    private static void ConfigureInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbContextTransaction, DbContextTransaction>();
    }
}
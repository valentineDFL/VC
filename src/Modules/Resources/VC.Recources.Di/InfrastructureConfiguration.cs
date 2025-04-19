using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VC.Recources.Domain;
using VC.Recources.Domain.UnitOfWork;
using VC.Recources.Infrastructure;
using VC.Recources.Infrastructure.Repositories;
using DbContext = VC.Recources.Infrastructure.DbContext.DbContext;

namespace VC.Recources.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureResourcesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<DbContext>(options => options
            .UseNpgsql(connectionString));

        ConfigureInfrastructure(services);
    }

    private static void ConfigureInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IResourcesUnitOfWork, UnitOfWork>();
    }
}
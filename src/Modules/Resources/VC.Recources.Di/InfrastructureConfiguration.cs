using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VC.Recources.Application;
using VC.Recources.Domain.UnitOfWork;
using VC.Recources.Infrastructure;
using VC.Recources.Infrastructure.Repositories;
using VC.Utilities.Resolvers;

namespace VC.Recources.Di;

public static class InfrastructureConfiguration
{
    public static void ConfigureResourcesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<ResourceDbContext>(options => options
            .UseNpgsql(connectionString));

        ConfigureInfrastructure(services);
    }

    private static void ConfigureInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}

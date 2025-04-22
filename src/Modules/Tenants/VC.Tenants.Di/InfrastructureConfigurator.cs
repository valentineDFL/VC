using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Infrastructure;
using VC.Tenants.Infrastructure.Persistence;
using VC.Tenants.Infrastructure.Persistence.Repositories;
using VC.Tenants.Repositories;
using VC.Tenants.UnitOfWork;

namespace VC.Tenants.Di;

internal static class InfrastructureConfigurator
{
    public static void ConfigureTenantsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<TenantsDbContext>(options => options
            .UseNpgsql(connectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", TenantsDbContext.SchemaName)));

        ConfigureRepositories(services);
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IDbSaver, DbSaver>();
    }
}
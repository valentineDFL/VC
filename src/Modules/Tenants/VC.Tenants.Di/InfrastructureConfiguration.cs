using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Infrastructure;
using VC.Tenants.Infrastructure.Persistence;
using VC.Tenants.Infrastructure.Persistence.Repositories;
using VC.Tenants.Repositories;
using VC.Tenants.UnitOfWork;

namespace VC.Tenants.Di;

public static class InfrastructureConfiguration
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<TenantsDbContext>(options => options.UseNpgsql(connectionString));

        ConfigureRepositories(services);
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IDbSaver, DbSaver>();
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VC.Recources.Infrastructure;

namespace VC.Recources.Di;

public static class InfrastructureConfiguration
{
    public static void ConfigureResourcesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<ResourceDbContext>(options => options
            .UseNpgsql(connectionString));
    }
}

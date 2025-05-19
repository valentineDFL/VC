using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VC.Orders.Infrastructure.Persistence;

namespace VC.Orders.Di;

public static class InfrastructureConfiguration
{
    public static void ConfigureInsrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSql");

        serviceCollection.AddDbContext<DatabaseDbContext>(options => options
        .UseNpgsql(connectionString, x =>
        {
            x.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseDbContext.Schema);
        }));
    }
}
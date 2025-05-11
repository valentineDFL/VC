using Microsoft.Extensions.DependencyInjection;
using VC.Core.Employees;

namespace VC.Core.Di;

public static class DomainConfiguration
{
    public static void ConfigureDomain(this IServiceCollection services)
    {
        services.AddScoped<AvailableSlotsGenerator>();
    }
}
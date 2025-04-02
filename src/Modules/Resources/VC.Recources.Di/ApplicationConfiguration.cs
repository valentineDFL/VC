using Microsoft.Extensions.DependencyInjection;
using VC.Recources.Application.Services;

namespace VC.Recources.Di;

public static class ApplicationConfiguration
{
    public static void ConfigureResourcesApplication(this IServiceCollection services)
    {
        services.AddScoped<IResourceService, ResourceService>();
    }
}

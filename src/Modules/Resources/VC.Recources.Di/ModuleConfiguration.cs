using Microsoft.Extensions.DependencyInjection;

namespace VC.Recources.Di
{
    public static class ModuleConfiguration
    {
        public static IServiceCollection ConfigureResourceModule(this IServiceCollection services )
        {
            return services;
        }
    }
}

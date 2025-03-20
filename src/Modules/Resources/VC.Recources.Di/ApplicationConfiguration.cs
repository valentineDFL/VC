using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Recources.Application.Services;

namespace VC.Recources.Di
{
    public static class ApplicationConfiguration
    {
        public static void ConfigureResourcesApplication(this IServiceCollection services)
        {
            services.AddScoped<IResourceSevice, ResourceService>();
        }
    }
}

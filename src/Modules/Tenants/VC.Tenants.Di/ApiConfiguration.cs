using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Api.Endpoints.Tenants.Models.Request;
using VC.Tenants.Api.Validation;

namespace VC.Tenants.Di;

public static class ApiConfiguration
{
    public static void ConfigureTenantsApiExtensions(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateTenantRequest>, CreateTenantValidation>();
        services.AddScoped<IValidator<UpdateTenantRequest>, UpdateTenantValidation>();
    }
}
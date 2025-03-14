using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Api.Endpoints.Tenants.Models;
using VC.Tenants.Api.Validation;

namespace VC.Tenants.Di;

public static class ApiConfiguration
{
    public static void ConfigureApiExtensions(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateTenantRequest>, CreateTenantValidation>();
        services.AddScoped<IValidator<UpdateTenantRequest>, UpdateTenantValidation>();
        services.AddHttpContextAccessor();
    }
}
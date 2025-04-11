using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VC.Tenants.Api.Endpoints.Tenants.Models.Request;
using VC.Tenants.Application.Tenants;
using VC.Utilities;

namespace VC.Tenants.Api.Endpoints.Tenants;

public static partial class TenantsEndpoints
{
    private static async Task<IResult> CreateAsync(
        [FromServices] ITenantsService tenantsService,
        [FromServices] ILoggerFactory loggerFactory,
        CreateTenantRequest request)
    {
        var logger = loggerFactory.CreateLogger(typeof(TenantsEndpoints));
        logger.LogInformation("Creating tenant {@Tenants}", request);

        var result = await tenantsService.CreateAsync(request.ToApplicationCreateDto());

        return result.ToMinimalApi();
    }
}
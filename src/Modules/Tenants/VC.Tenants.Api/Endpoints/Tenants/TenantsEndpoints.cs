using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VC.Tenants.Api.OpenApi;

namespace VC.Tenants.Api.Endpoints.Tenants;

public static partial class TenantsEndpoints
{
    public static IEndpointRouteBuilder AddTenantsEndpoints(this IEndpointRouteBuilder builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersions([new(1)])
            .ReportApiVersions()
            .Build();

        var group = builder.MapGroup("/api/v{version:apiVersion}")
            .WithTags("Tenants")
            .WithGroupName(OpenApiConfig.GroupName)
            .WithApiVersionSet(apiVersionSet);

        group.MapPost("tenants", CreateAsync)
            .WithOpenApi()
            .WithSummary("Создать арендатора")
            .WithDescription("Создает арендатора")
            .MapToApiVersion(1);
        
        return builder;
    }
}

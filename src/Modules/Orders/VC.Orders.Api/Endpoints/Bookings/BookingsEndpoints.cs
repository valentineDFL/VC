using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VC.Orders.Api.OpenApi;

namespace VC.Orders.Api.Endpoints.Orders;

public static partial class BookingsEndpoints
{
    public static IEndpointRouteBuilder AddBookingsEndpoints(this IEndpointRouteBuilder builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersions([new(2)])
            .ReportApiVersions()
            .Build();
        
        var group = builder.MapGroup("/api/v{version:apiVersion}")
            .WithGroupName(OpenApiConfig.GroupName)
            .WithApiVersionSet(apiVersionSet);
        
        group.MapPost("bookings", () => Results.Empty).MapToApiVersion(2);
        return builder;
    }
}

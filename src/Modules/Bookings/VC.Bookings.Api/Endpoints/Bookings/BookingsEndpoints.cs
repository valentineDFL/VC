using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using VC.Bookings.Api.OpenApi;

namespace VC.Bookings.Api.Endpoints.Bookings;

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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace VC.Bookings.Api.Endpoints.Bookings;

public static partial class BookingsEndpoints
{
    public static IEndpointRouteBuilder AddBookingsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/bookings", () => Results.Empty);
        return builder;
    }
}

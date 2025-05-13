using Microsoft.AspNetCore.Http;

namespace VC.Auth.Api.Middleware;

public class TenantMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        => await next(context);
}
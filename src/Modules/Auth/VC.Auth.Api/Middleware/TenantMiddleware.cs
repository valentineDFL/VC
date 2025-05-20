using Microsoft.AspNetCore.Http;
using VC.Auth.Application;

namespace VC.Auth.Api.Middleware;

public class TenantMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context) => await _next(context);
}
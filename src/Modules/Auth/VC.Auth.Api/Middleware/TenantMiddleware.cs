using Microsoft.AspNetCore.Http;
using VC.Auth.Application;

namespace VC.Auth.Api.Middleware;

public class TenantMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        var tenantId =
            context.Request.Headers["X-Tenant-ID"].FirstOrDefault()
            ?? context.Request.Query["tenantId"].FirstOrDefault();

        if (string.IsNullOrEmpty(tenantId) &&
            context.User.Identity?.IsAuthenticated == true)
        {
            tenantId = context.User.FindFirst("tenant")?.Value;
        }

        if (string.IsNullOrEmpty(tenantId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("X-Tenant-ID header is required");
            return;
        }

        tenantContext.CurrentTenant = tenantId;

        await _next(context);
    }
}
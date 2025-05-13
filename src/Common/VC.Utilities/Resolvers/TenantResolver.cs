using Microsoft.AspNetCore.Http;

namespace VC.Utilities.Resolvers;

public class TenantResolver(IHttpContextAccessor _context) : ITenantResolver
{
    public Guid Resolve()
    {
        // заглушка до времён реализации аутентификации через JWT + Cookie

        var tenantClaim = _context.HttpContext?.User.Claims.FirstOrDefault();
        return tenantClaim is not null ? Guid.Parse(tenantClaim.Value) : Guid.Empty;
    }
}
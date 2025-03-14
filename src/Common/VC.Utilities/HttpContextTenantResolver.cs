using Microsoft.AspNetCore.Http;

namespace VC.Utilities;

public class HttpContextTenantResolver : ITenantResolver
{
    private readonly IHttpContextAccessor _context;
    // dbearer

    public HttpContextTenantResolver(IHttpContextAccessor context)
    {
        _context = context;
    }

    public Guid Resolve()
    {
        //var test = _context.HttpContext.Request.Cookies[CookieNames.AuthCookie];

        _context.HttpContext.Request.Headers.TryGetValue("Jopa", out var value);
        
        return Guid.Empty;
    }
}

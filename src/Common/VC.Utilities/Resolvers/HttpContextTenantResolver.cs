using Microsoft.AspNetCore.Http;

namespace VC.Utilities.Resolvers;

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
        // заглушка до времён реализации аутентификации через JWT + Cookie

        //var test = _context.HttpContext.Request.Cookies[CookieNames.AuthCookie];

        _context.HttpContext.Request.Headers.TryGetValue("Jopa", out var value);

        var ids = new List<Guid>()
        {
            new Guid("195655b5-83c9-45e9-8c9c-ddfcebbd0471"),
            new Guid("893b9289-31a4-42bf-9d3d-4b04bbb6e5de"),
            new Guid("89aae19d-e209-4fcd-b722-aaca74fc5490"),
            new Guid("d9eb5e78-0f06-4a2a-8060-63adb51177c6"),
            new Guid("ed7fedb8-9ece-4360-abf4-ab6e0165f96c"), // +
            new Guid("efe58ddd-6410-497b-a2ec-2e319aa41e43")
        };

        Guid id = ids[new Random().Next(0, ids.Count - 1)];
        Console.WriteLine("Вот что должно вывестись в каждом запросе: " + id);

        return id;
    }
}

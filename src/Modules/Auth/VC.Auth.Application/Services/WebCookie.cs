using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using VC.Auth.Application.Abstractions;
using VC.Shared.Utilities.Options.Jwt;

namespace VC.Auth.Application.Services;

public class WebCookie : IWebCookie
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookiesSettings _cookiesSettings;

    public WebCookie(IHttpContextAccessor httpContextAccessor, IOptions<CookiesSettings> cookiesSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _cookiesSettings = cookiesSettings.Value;
    }

    public void AddSecure(string cookieName, string value)
    {
        CookieOptions options = new CookieOptions();
        options.Path = "/";
        options.HttpOnly = true;
        options.Secure = true;

        var days = _cookiesSettings.CookieLifeTimeInDays;

        options.Expires = DateTimeOffset.UtcNow.AddDays(days);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public async Task<Result> DeleteAsync(string cookieName)
    {
        if (string.IsNullOrEmpty(cookieName) || string.IsNullOrWhiteSpace(cookieName))
            return Result.Fail("Cookie name is null");

        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(cookieName);

        return Result.Ok();
    }

    public async Task<Result<string>> GetAsync(string cookieName)
    {
        if (cookieName is null)
            return Result.Fail("Cookie name is null");

        var cookie =
            _httpContextAccessor.HttpContext?.Request.Cookies
                .FirstOrDefault(m => m.Key == cookieName);

        if (cookie is null || cookie.Value.Value is null)
            return Result.Fail("Cookie not found");

        return Result.Ok(cookie.Value.Value);
    }
}
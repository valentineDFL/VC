using FluentResults;

namespace VC.Auth.Application.Abstractions;

public interface IWebCookie
{
    void AddSecure(string cookieName, string token);

    Task<Result> DeleteAsync(string cookieName);

    Task<Result<string>> Get(string cookieName);
}
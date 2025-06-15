using FluentResults;

namespace VC.Auth.Application.Abstractions;

public interface IWebCookie
{
    void AddSecure(string cookieName, string value);

    Task<Result> DeleteAsync(string cookieName);

    Task<Result<string>> GetAsync(string cookieName);
}
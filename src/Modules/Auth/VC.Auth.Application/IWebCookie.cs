namespace VC.Auth.Application;

public interface IWebCookie
{
    void AddSecure(string cookieName, string token, int days = 0);

    Task Delete(string cookieName);

    string? Get(string cookieName);
}
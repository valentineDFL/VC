namespace VC.Auth.Infrastructure.Persistence.Models;

public class CookiesSettings
{
    public const string Cookie = "RememberMe";

    public string RememberMeCookieName { get; set; } = string.Empty;

    public int RememberMeDays { get; set; }
}
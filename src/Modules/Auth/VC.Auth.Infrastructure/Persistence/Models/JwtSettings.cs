namespace VC.Auth.Infrastructure.Persistence.Models;

public class JwtSettings
{
    public const string Jwt = "Jwt";

    public string SecretKey { get; set; } 

    public int ExpiresTime { get; set; }
}
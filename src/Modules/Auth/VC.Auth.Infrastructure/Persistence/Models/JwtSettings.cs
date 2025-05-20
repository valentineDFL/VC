namespace VC.Auth.Infrastructure.Persistence.Models;

public class JwtSettings
{
    public string SecretKey { get; set; }
    
    public int ExpiresTime{ get; set; }
}
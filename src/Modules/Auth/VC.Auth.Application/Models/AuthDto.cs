namespace VC.Auth.Application.Models;

public class AuthDto
{
    public Guid TenantId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
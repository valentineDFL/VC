namespace VC.Auth.Api.Models.Requests;

public class SignUpRequest
{
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}
namespace VC.Auth;

public interface IPasswordHashHandler
{
    string HashPassword(string password, string salt);
}
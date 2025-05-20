namespace VC.Auth.Interfaces;

public interface IPasswordHashHandler
{
    string HashPassword(string password, string? salt);
}
namespace VC.Auth.Interfaces;

public interface IEncrypter
{
    string HashPassword(string password, string? salt);
}
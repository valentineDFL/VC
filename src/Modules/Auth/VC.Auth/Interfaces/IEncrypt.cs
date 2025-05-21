namespace VC.Auth.Interfaces;

public interface IEncrypt
{
    string HashPassword(string password, string? salt);
}
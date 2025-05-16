using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace VC.Auth.Api.Handlers;

public class PasswordHashHandler : IPasswordHashHandler
{
    public string HashPassword(string password, string? salt)
        => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            System.Text.Encoding.ASCII.GetBytes(salt!),
            KeyDerivationPrf.HMACSHA512,
            5000,
            64
        ));
}
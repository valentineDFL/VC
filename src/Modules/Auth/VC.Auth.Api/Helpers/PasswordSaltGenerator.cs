using System.Security.Cryptography;
using VC.Auth.Interfaces;

namespace VC.Auth.Api.Helpers;

public class PasswordSaltGenerator : IPasswordSaltGenerator
{
    public string Generate()
    {
        byte[] saltBytes = new byte[32];
        RandomNumberGenerator.Create().GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}
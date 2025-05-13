using System.Security.Cryptography;

namespace VC.Auth.Helpers;

public class PasswordHelper
{
    public static string GenerateSalt()
    {
        byte[] saltBytes = new byte[32];
        RandomNumberGenerator.Create().GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}
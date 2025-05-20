using VC.Auth.Models;

namespace VC.Auth.Interfaces;

public interface IJwtOptions
{
    string GenerateToken(User user);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Infrastructure.Persistence.Models;
using VC.Auth.Interfaces;
using VC.Auth.Models;

namespace VC.Auth.Infrastructure;

public class JwtOptions(JwtSettings _jwtSettings) : IJwtOptions
{
    public string GenerateToken(User user)
    {
        Claim[] claims = [new("tenantId", user.TenantId.ToString())];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiresTime)
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
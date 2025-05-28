using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Constants;
using VC.Auth.Infrastructure.Persistence.Models;
using VC.Auth.Interfaces;
using VC.Auth.Models;

namespace VC.Auth.Infrastructure;

public class JwtOptions : IJwtOptions
{
    private readonly JwtSettings _jwtSettings;

    public JwtOptions(IOptions<JwtSettings> jwtOptions)
        => _jwtSettings = jwtOptions.Value;

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Id, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, JwtClaimTypes.User),
            new Claim(ClaimTypes.Role, JwtClaimTypes.Tenant)
        };

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
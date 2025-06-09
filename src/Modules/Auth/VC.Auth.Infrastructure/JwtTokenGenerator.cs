using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Shared.Utilities.Constants;
using VC.Shared.Utilities.Options.Jwt;

namespace VC.Auth.Infrastructure;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        => _jwtSettings = jwtOptions.Value;

    public string GenerateToken(Dictionary<string, string> datas)
    {
        var claims = new List<Claim>();

        foreach(var item in datas)
            claims.Add(new Claim(item.Key, item.Value));

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
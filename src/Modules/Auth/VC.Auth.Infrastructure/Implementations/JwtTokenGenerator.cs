using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Interfaces;
using VC.Shared.Utilities.Options.Jwt;

namespace VC.Auth.Infrastructure.Implementations;

internal class HmacSha256JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public HmacSha256JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        => _jwtSettings = jwtOptions.Value;

    public string GenerateToken(Dictionary<string, string> datas, DateTime expireTime)
    {
        if (expireTime < DateTime.UtcNow)
            throw new ArgumentException("Expire time cannot be in the past");

        var claims = new List<Claim>();

        foreach(var item in datas)
            claims.Add(new Claim(item.Key, item.Value));

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: expireTime
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
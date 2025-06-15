using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using VC.Auth.Application.Abstractions;
using VC.Shared.Utilities.Options.Jwt;

namespace VC.Auth.Infrastructure.Implementations;

public class JwtTokenValidator : IJwtTokenValidator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenValidator(IOptions<JwtSettings> options)
        => _jwtSettings = options.Value;

    public async Task<(bool IsValid, Dictionary<string, object>? Claims)> ValidateAsync(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        var securityKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidIssuer = securityKey.ToString()
        };

        var validationResult = await tokenHandler.ValidateTokenAsync(token, validationParameters);
        if (!validationResult.IsValid)
            return (false, null);

        return (true, validationResult.Claims.ToDictionary());
    }
}
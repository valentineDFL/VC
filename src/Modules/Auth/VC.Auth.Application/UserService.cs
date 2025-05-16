using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Application.Models.Requests;
using VC.Auth.Models;
using VC.Auth.Repositories;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace VC.Auth.Application;

public class UserService(
    IConfiguration _configuration,
    IUserRepository _userRepository,
    IPasswordHashHandler _passwordHashHandler,
    ITenantContext _tenantContext) : IUserService
{
    public async Task<Result> Register(RegisterRequest request)
    {
        var user = new User
        {
            TenantId = Guid.CreateVersion7(),
            Username = request.Username.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            PasswordHash = _passwordHashHandler.HashPassword(
                request.Password,
                GeneratePasswordSalt()
            )
        };

        await _userRepository.CreateAsync(user);

        return Result.Ok();
    }

    public async Task<Result<string>> Login(LoginRequest request)
    {
        if (string.IsNullOrEmpty(_tenantContext.CurrentTenant))
            return Result.Fail<string>("Tenant not specified");

        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return Result.Fail("Invalid email or password");
        }

        var user = await _userRepository.GetByEmailAsync(request.Email, _tenantContext.CurrentTenant);
        if (user is null)
            return Result.Fail("User not found");

        var inputPassword = _passwordHashHandler.HashPassword(request.Password, GeneratePasswordSalt());

        if (user.PasswordHash != inputPassword)
            return Result.Fail("Password is incorrect");

        var accessToken = GenerateJwtToken(user);
        return Result.Ok(accessToken);
    }

    public Task<Result> Logout(LoginRequest request)
    {
        // delete cookie 
        throw new NotImplementedException();
        // return Result.Ok();
    }

    private string GeneratePasswordSalt()
    {
        byte[] saltBytes = new byte[32];
        RandomNumberGenerator.Create().GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }

    private string GenerateJwtToken(User user)
    {
        var issuer = _configuration["JwtConfig:Issue"];
        var audience = _configuration["JwtConfig:Audience"];
        var key = _configuration["JwtConfig:Key"];
        var tokenValidityMinutes = _configuration.GetValue<int>("JwtConfig:TokenValidityMinutes");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }),
            Expires = tokenExpiryTimeStamp,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}
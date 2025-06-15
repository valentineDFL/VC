using FluentResults;
using VC.Auth.Application.Models;

namespace VC.Auth.Application.Abstractions;

public interface IAuthService
{
    Task<Result> SignUpAsync(RegisterAuthParams authParams);
    
    Task<Result> LoginAsync(LoginAuthParams authParams);

    Task<Result> RefreshTokenAsync();

    Task<Result> LogoutAsync();
}
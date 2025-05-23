using FluentResults;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Application.Abstractions;

public interface IAuthService
{
    Task<Result> Register(RegisterRequest request);
    
    Task<Result<string>> Login(LoginRequest request);
    
    Task Logout();
}
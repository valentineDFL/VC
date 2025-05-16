using FluentResults;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Application;

public interface IUserService
{
    Task<Result> Register(RegisterRequest request);
    
    Task<Result<string>> Login(LoginRequest request);
    
    Task<Result> Logout(LoginRequest request);
}
using VC.Auth.Application.UseCases.Models.Requests;
using VC.Auth.Application.UseCases.Models.Responses;

namespace VC.Auth.Application.UseCases;

public interface IJwtService
{
    Task<LoginResponse> Login(LoginRequest request);
    
    
    
}
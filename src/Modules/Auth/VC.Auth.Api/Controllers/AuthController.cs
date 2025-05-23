using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VC.Auth.Api.Validations;
using VC.Auth.Application;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Validators;
using LoginRequest = VC.Auth.Application.Models.Requests.LoginRequest;
using RegisterRequest = VC.Auth.Application.Models.Requests.RegisterRequest;

namespace VC.Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        var validator = new RegistrationValidation();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await authService.Register(request);

        if (response is null)
            return BadRequest("Registration failed");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var validator = new LoginValidation();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await authService.Login(request);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        var result = authService.Logout();
        return Ok(result);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VC.Auth.Api.Validations;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models;
using VC.Auth.Application.Validators;

namespace VC.Auth.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class AuthsController(IAuthService authService) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp(RegisterAuthParams authParams)
    {
        var validator = new SignUpValidation();
        var result = await validator.ValidateAsync(authParams);
        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await authService.SignUpAsync(authParams);

        if (response is null)
            return BadRequest("Sign up failed");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginAuthParams authParams)
    {
        var validator = new LoginValidation();
        var result = await validator.ValidateAsync(authParams);
        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await authService.LoginAsync(authParams);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        var result = authService.LogoutAsync();
        return Ok(result);
    }
}
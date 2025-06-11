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
    public async Task<ActionResult> SignUpAsync(RegisterAuthParams authParams)
    {
        var validator = new SignUpValidator();
        var result = await validator.ValidateAsync(authParams);
        if (!result.IsValid)
            return BadRequest(result);

        var registrationResult = await authService.SignUpAsync(authParams);

        if (!registrationResult.IsSuccess)
            return BadRequest(registrationResult);

        return Ok(registrationResult);
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(LoginAuthParams authParams)
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
    public async Task<ActionResult> LogoutAsync()
    {
        var result = await authService.LogoutAsync();

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
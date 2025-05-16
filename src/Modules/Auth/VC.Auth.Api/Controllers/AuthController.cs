using Microsoft.AspNetCore.Mvc;
using VC.Auth.Api.Handlers;
using VC.Auth.Api.Models.Requests;
using VC.Auth.Api.Validations;
using VC.Auth.Application;
using VC.Auth.Application.Validators;
using VC.Auth.Models;
using VC.Auth.Repositories;
using VC.Utilities;
using LoginRequest = VC.Auth.Application.Models.Requests.LoginRequest;
using RegisterRequest = VC.Auth.Application.Models.Requests.RegisterRequest;

namespace VC.Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class AuthController(IUserService _userService, IUserRepository _userRepository) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        var validator = new RegistrationValidation();

        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await _userService.Register(request);

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

        var response = await _userService.Login(request);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("logout")]
    public ActionResult Logout()
    {
        // HttpContext.Response.Cookies.Delete(CookiesCodes.AuthCookie); 
        return Ok();
    }
}
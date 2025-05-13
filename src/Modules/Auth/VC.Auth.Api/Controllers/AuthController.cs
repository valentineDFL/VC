using Microsoft.AspNetCore.Mvc;
using VC.Auth.Application.UseCases;
using LoginRequest = VC.Auth.Application.UseCases.Models.Requests.LoginRequest;

namespace VC.Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class AuthController(IJwtService _jwtService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginRequest>> Login(LoginRequest request)
    {
        var result = await _jwtService.Login(request);
        if (result is null)
            return BadRequest("Invalid login request");

        return Ok(result);
    }
}
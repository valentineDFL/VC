using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VC.Auth.Application;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
[Authorize(Roles = Permissions.User)]
public class UserController(IUserOrderService _userOrderService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult> CreateOrder(CreateOrderRequest request)
    {
        var result = await _userOrderService.CreateUserOrderAsync(request);

        return Ok(result);
    }

    [HttpGet("available-slots")]
    public async Task<ActionResult> GetAvailableSlots(
        Guid serviceId,
        DateTime fromDate,
        DateTime toDate)
    {
        var userId = GetCurrentUserId();
        var slots = await _userOrderService.GetAvailableSlotsAsync(
            serviceId,
            userId,
            fromDate,
            toDate
        );

        return Ok(slots);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> CancelOrder(Guid id)
    {
        var userId = GetCurrentUserId();
        await _userOrderService.CancelOrderAsync(id, userId);
        return NoContent();
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            throw new SecurityException("Invalid user identity");
        }

        return userId;
    }
}
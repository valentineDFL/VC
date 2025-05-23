using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VC.Auth.Application;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
[Authorize(Roles = Permissions.Tenant)]
public class TenantController(ITenantService _tenantService) : ControllerBase
{
    [HttpPost("services")]
    public async Task<ActionResult> AddService(CreateServiceRequest request)
    {
        var service = _tenantService.CreateServiceAsync(request);
        return Ok(service);
    }

    [HttpGet("services/{id}")]
    public async Task<ActionResult> GetService(Guid id)
    {
        var service = _tenantService.GetServiceAsync(id);
        return service != null ? Ok(service) : NotFound();
    }

    [HttpPut("schedule")]
    public async Task<ActionResult> UpdateSchedule(UpdateScheduleRequest request)
    {
        await _tenantService.UpdateScheduleAsync(request);
        return NoContent();
    }
}
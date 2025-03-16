using Microsoft.AspNetCore.Mvc;
using VC.Recources.Resource.Domain.Entities;
using VC.Resources.Api.Endpoints;
using VC.Tenants.Application.Tenants;

namespace VC.Resources.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    private readonly IResourceSevice _resourceSevice;
    private readonly ITenantsService _tenantService;

    public ResourceController(
        IResourceSevice resourceSevice,
        ITenantsService tenantService
        )
    {
        _resourceSevice = resourceSevice;
        _tenantService = tenantService;
    }

    [HttpGet]
    public async Task<ActionResult> GetResourcesAsync(Guid id)
    {
        var tenantResult = await _tenantService.GetAsync();

        if (!tenantResult.IsSuccess)
            return BadRequest(tenantResult);

        var resource = await _resourceSevice.GetResourceAsync(
            tenantResult.Value.Id, id);

        return Ok(resource);
    }

    [HttpPost]
    public async Task<ActionResult> CreateResourcesAsync(CreateResourceRequest request)
    {
        var tenantResult = await _tenantService.GetAsync();

        if (!tenantResult.IsSuccess)
            return BadRequest(tenantResult);

        var resource = await _resourceSevice.CreateResourceAsync(
            tenantResult.Value.Id,
            request.Name,
            request.Description,
            request.ResourceType,
            request.Attributes
            );

        return CreatedAtAction(
            nameof(GetResourcesAsync),
            MapToResponse(resource));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateResourcesAsync(Guid id, UpdateResourceRequest request)
    {
        var result = await _resourceSevice.UpdateResourceAsync(
            id,
            request.Name,
            request.Description,
            request.Attributes
            );

        return result ? NoContent() : NotFound();
    }

    private ResourceResponse MapToResponse(Resource resource)
    {
        return new()
        {
            Id = resource.Id,
            Name = resource.Name,
            Description = resource.Description,
            ResourceType = resource.ResourceType,
            Attributes = resource.Attributes
        };
    }
}

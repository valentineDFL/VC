using Microsoft.AspNetCore.Mvc;
using VC.Tenants.Application.Tenants;
using VC.Recources.Application.Services;
using VC.Resources.Api.Endpoints.Models.Request;
using VC.Resources.Api.Endpoints.Models.Response;
using VC.Resources.Api.Endpoints;

namespace VC.Resources.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    private readonly IResourceSevice _resourceService;
    private readonly ITenantsService _tenantService;

    public ResourceController(
        IResourceSevice resourceSevice,
        ITenantsService tenantService
        )
    {
        _resourceService = resourceSevice;
        _tenantService = tenantService;
    }

    [HttpGet]
    public async Task<ActionResult<ResourceResponseDto>> GetResourceAsync(Guid id)
    {
        var tenantResult = await _tenantService.GetAsync();

        if (!tenantResult.IsSuccess)
            return BadRequest(tenantResult);

        var response = await _resourceService.GetResourceAsync(tenantResult.Value.Id);

        if (!response.IsSuccess)
            return BadRequest(response);

        var mappedResource = response.Value.ToResponseDto();

        return Ok(mappedResource);
    }

    [HttpPost]
    public async Task<ActionResult> CreateResourceAsync(CreateResourceRequest dto)
    {
        var tenantResult = await _tenantService.GetAsync();

        if (!tenantResult.IsSuccess)
            return BadRequest(tenantResult);

        var mappedDto = dto.ToCreateResourceDto();
        var response = await _resourceService.CreateResourceAsync(mappedDto);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateResourceAsync(UpdateResourceRequest dto)
    {
        var mappedDto = dto.ToUpdateResourceDto();
        var response = await _resourceService.UpdateResourceAsync(mappedDto);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }
}

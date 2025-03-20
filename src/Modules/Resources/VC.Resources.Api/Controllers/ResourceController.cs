using Microsoft.AspNetCore.Mvc;
using VC.Recources.Application.Services;
using VC.Resources.Api.Endpoints.Models.Request;
using VC.Resources.Api.Endpoints.Models.Response;
using VC.Utilities.Resolvers;

namespace VC.Resources.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class ResourceController : ControllerBase
{
    private readonly IResourceSevice _resourceService;
    private readonly ITenantResolver _tenantResolver;

    public ResourceController(
        IResourceSevice resourceSevice,
        ITenantResolver tenantResolver)
    {
        _tenantResolver = tenantResolver;
        _resourceService = resourceSevice;
    }

    [HttpGet]
    public async Task<ActionResult<ResourceResponse>> GetResourceAsync(Guid id)
    {
        var tenantId = _tenantResolver.Resolve();

        var response = await _resourceService.GetResourceAsync(tenantId);

        if (!response.IsSuccess)
            return BadRequest(response);

        var mappedResource = response.Value.ToApiResource();

        return Ok(mappedResource);
    }

    [HttpPost]
    public async Task<ActionResult> CreateResourceAsync(CreateResourceRequest dto)
    {
        var tenantId = _tenantResolver.Resolve();

        var mappedDto = dto.ToCreateResourceDto(tenantId);
        var response = await _resourceService.CreateResourceAsync(mappedDto);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateResourceAsync(UpdateResourceRequest dto)
    {
        var tenantId = _tenantResolver.Resolve();

        var mappedDto = dto.ToUpdateResourceDto();
        var response = await _resourceService.UpdateResourceAsync(mappedDto);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }
}

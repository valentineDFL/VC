using Microsoft.AspNetCore.Mvc;
using VC.Recources.Application.Services;
using VC.Resources.Api.Endpoints.Models.Request;
using VC.Resources.Api.Endpoints.Models.Response;
using VC.Utilities.Resolvers;

namespace VC.Resources.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class ResourcesController : ControllerBase
{
    private readonly IResourceSevice _resourceService;
    private readonly ITenantResolver _tenantResolver; // вынести в application

    public ResourcesController(
        IResourceSevice resourceSevice,
        ITenantResolver tenantResolver)
    {
        _tenantResolver = tenantResolver;
        _resourceService = resourceSevice;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResourceResponse>> GetResourceAsync(Guid id)
    {
        var response = await _resourceService.GetResourceAsync(id);

        if (!response.IsSuccess)
            return BadRequest(response);

        var mappedResource = response.Value.ToResponseDto();

        return Ok(mappedResource);
    }

    [HttpPost]
    public async Task<ActionResult> CreateResourceAsync(CreateResourceRequest dto)
    {
        var tenantId = _tenantResolver.Resolve();
        var mappedDto = dto.ToCreateResourceDto(tenantId); // логику присвоения ресурса tenantId перенести в метод Сервиса (Application)
        
        var response = await _resourceService.CreateResourceAsync(mappedDto);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateResourceAsync(UpdateResourceRequest dto)
    {
        var tenantId = _tenantResolver.Resolve();
        var mappedDto = dto.ToUpdateResourceDto(tenantId);
        
        var response = await _resourceService.UpdateResourceAsync(mappedDto);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }
}

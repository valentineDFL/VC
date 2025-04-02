using Microsoft.AspNetCore.Mvc;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Application.Services;
using VC.Resources.Api.Endpoints.Models.Requests;
using VC.Resources.Api.Endpoints.Models.Response;
using VC.Utilities;
using VC.Utilities.Resolvers;

namespace VC.Resources.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class ResourcesController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourcesController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<ResourceResponse>> GetResourceAsync(Guid id)
    {
        var response = await _resourceService.GetResourceAsync(id);

        if (!response.IsSuccess)
            return BadRequest(response);

        var mappedResource = response.Value.ToResponseDto();

        return Ok(mappedResource);
    }

    [HttpPost]
    public async Task<ActionResult> CreateResourceAsync(CreateResourceRequest request)
    {
        var dto = new CreateResourceDto(
            request.Name,
            request.Description,
            request.Skills
        );
        
        var response = await _resourceService.CreateResourceAsync(dto);
        
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateResourceAsync(UpdateResourceRequest request)
    {
        var dto = new UpdateResourceDto(
            request.Id,
            request.Name,
            request.Description,
            request.Skills
            );
        
        var response = await _resourceService.UpdateResourceAsync(dto);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
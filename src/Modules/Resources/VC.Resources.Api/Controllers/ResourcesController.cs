using Microsoft.AspNetCore.Mvc;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Application.Helpers;
using VC.Recources.Application.Services;
using VC.Recources.Application.Validators;
using VC.Resources.Api.Endpoints.Models.Requests;
using VC.Resources.Api.Endpoints.Models.Response;

namespace VC.Resources.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class ResourcesController(IResourceService _resourceService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateResourceRequest request)
    {
        var dto = new CreateResourceDto(
            request.Name,
            request.Description,
            request.Skills
        );

        var validator = new CreateResourceDtoValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await _resourceService.CreateAsync(dto);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

<<<<<<< HEAD
    [Route("{id}")]
    [HttpGet]
=======
    [HttpGet("{id:Guid}")]
<<<<<<< HEAD
>>>>>>> origin/feature/VC-8/ResourceModule
=======
>>>>>>> origin/feature/VC-8/ResourceModule
    public async Task<ActionResult<ResourceResponse>> GetAsync(Guid id)
    {
        var response = await _resourceService.GetAsync(id);

        if (!response.IsSuccess)
            return BadRequest(response);

        var mappedResource = response.Value.ToResponseDto();

        return Ok(mappedResource);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateResourceRequest request)
    {
        var dto = new UpdateResourceDto(
            request.Id,
            request.Name,
            request.Description,
            request.Skills
        );

        var validator = new UpdateResourceDtoValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await _resourceService.UpdateAsync(dto);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest();
    }
}
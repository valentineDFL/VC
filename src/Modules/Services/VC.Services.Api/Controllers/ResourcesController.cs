using VC.Services.Api.Models.Resources.CreateResource;
using VC.Services.Api.Models.Resources.UpdateResource;
using VC.Services.Api.Validations;
using VC.Services.Application.ResourcesUseCases;
using VC.Services.Application.ResourcesUseCases.Models;
using VC.Services.Application.ResourcesUseCases.Validators;
using VC.Services.Repositories;

namespace VC.Services.Api.Controllers;

[Route("api/v1/resources")]
public class ResourcesController(
    IResourcesService _resourcesService) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Resource>> GetAsync(Guid id)
    {
        var result = await _resourcesService.GetAsync(id);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateResourceRequest req)
    {
        var parameters = new CreateResourceParams(req.Title, req.Description, req.Count);
        var validator = new CreateResourceDtoValidator();
        var validationResult = await validator.ValidateAsync(parameters);
        if (!validationResult.IsValid)
            return validationResult.ToErrorActionResult();

        var result = await _resourcesService.CreateAsync(parameters);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateResourceRequest req)
    {
        var parameters = new UpdateResourceParams(id, req.Title, req.Description, req.Count);

        var validator = new UpdateResourceDtoValidator();
        var validationResult = await validator.ValidateAsync(parameters);
        if (!validationResult.IsValid)
            return validationResult.ToErrorActionResult();

        var result = await _resourcesService.UpdateAsync(parameters);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemoveAsync(Guid id)
    {
        var result = await _resourcesService.RemoveAsync(id);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }
}
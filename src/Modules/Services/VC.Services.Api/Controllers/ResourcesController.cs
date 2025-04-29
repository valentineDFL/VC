using Microsoft.AspNetCore.Mvc;
using VC.Services.Api.Models.Resources.CreateResource;
using VC.Services.Api.Models.Resources.Responses;
using VC.Services.Api.Models.Resources.UpdateResource;
using VC.Services.Api.Validations;
using VC.Services.Application.ResourcesUseCases;
using VC.Services.Application.ResourcesUseCases.Validators;
using VC.Services.Repositories;

namespace VC.Services.Api.Controllers;

[Route("api/resources")]
public class ResourcesController(
    IResourcesService _resourcesService,
    IUnitOfWork _unitOfWork) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response>> GetAsync(Guid id)
    {
        var response = await _resourcesService.GetAsync(id);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        var mappedResource = response.Value.ToResponseDto();

        return Ok(mappedResource);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateResourceRequest req)
    {
        await _unitOfWork.BeginTransactionAsync();

        var dto = req.ToCreateParams();

        var validator = new CreateResourceDtoValidator();
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await _resourcesService.CreateAsync(dto);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        await _unitOfWork.CommitAsync();

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateResourceRequest req)
    {
        await _unitOfWork.BeginTransactionAsync();

        var dto = req.ToUpdateDto(id);

        var validator = new UpdateResourceDtoValidator();
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await _resourcesService.UpdateAsync(dto);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        await _unitOfWork.CommitAsync();

        return Ok(response);
    }
}
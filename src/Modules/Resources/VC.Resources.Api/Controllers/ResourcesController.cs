using Microsoft.AspNetCore.Mvc;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Application.Helpers;
using VC.Recources.Application.Interfaces;
using VC.Recources.Application.Validators;
using VC.Resources.Api.Endpoints.Models.Requests;
using VC.Resources.Api.Endpoints.Models.Responses;

namespace VC.Resources.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class ResourcesController(IService _service)
    : ControllerBase
{
    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult<Response>> GetAsync(Guid id)
    {
        var response = await _service.GetAsync(id);

        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        var mappedResource = response.Value.ToResponseDto();

        return Ok(mappedResource);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(CreateRequest request)
    {
        var dto = new CreateDto(
            request.Name,
            request.Description,
            request.Skills
        );

        var validator = new CreateResourceDtoValidator();
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await _service.AddAsync(dto);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(UpdateRequest request)
    {
        var dto = new UpdateDto(
            request.Id,
            request.Name,
            request.Description,
            request.Skills
        );

        var validator = new UpdateResourceDtoValidator();
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
            return result.ToErrorActionResult();

        var response = await _service.UpdateAsync(dto);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        return Ok(response);
    }
}
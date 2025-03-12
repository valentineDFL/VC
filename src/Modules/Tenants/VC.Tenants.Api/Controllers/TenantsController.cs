using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VC.Tenants.Api.Endpoints.Tenants.Models;
using VC.Tenants.Application.Tenants;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Controller;

[ApiController]
[Route("[Controller]")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class TenantsController : ControllerBase
{
    private readonly ITenantsService _tenantService;
    private readonly IValidator<CreateTenantRequest> _createTenantValidator;
    private readonly IValidator<UpdateTenantRequest> _updateTenantValidator;

    public TenantsController(ITenantsService tenantService,
        IValidator<CreateTenantRequest> createTenantValidator,
        IValidator<UpdateTenantRequest> updateTenantValidator)
    {
        _tenantService = tenantService;
        _createTenantValidator = createTenantValidator;
        _updateTenantValidator = updateTenantValidator;
    }

    [HttpGet]
    public async Task<ActionResult<Tenant>> GetByIdAsync([FromQuery] Guid id)
    {
        var response = await _tenantService.GetByIdAsync(id);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(CreateTenantRequest createRequest)
    {
        var validationResult = await _createTenantValidator.ValidateAsync(createRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var mappedCreateDto = createRequest.ToCreateTenantParams();

        var response = await _tenantService.CreateAsync(mappedCreateDto);

        if (response.IsSuccess)
            return Ok(response);


        return BadRequest(response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(UpdateTenantRequest updateRequest)
    {
        var validationResult = await _updateTenantValidator.ValidateAsync(updateRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult);

        var mappedUpdateDto = updateRequest.ToTenantUpdateDto();

        Result response = await _tenantService.UpdateAsync(mappedUpdateDto);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteByIdAsync([FromQuery] Guid id)
    {
        var response = await _tenantService.DeleteAsync(id);

        if (response.IsSuccess)
            return Ok();

        return BadRequest(response);
    }
}
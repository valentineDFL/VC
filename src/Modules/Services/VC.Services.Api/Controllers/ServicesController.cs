using Microsoft.AspNetCore.Mvc;
using VC.Services.Api.Models.Services;
using VC.Services.Application.ServicesUseCases;
using VC.Services.Application.ServicesUseCases.Models;
using VC.Services.Repositories;

namespace VC.Services.Api.Controllers;

[ApiController]
[Route("api/services")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class ServicesController(IServicesService _servicesService, IUnitOfWork _unitOfWork)
    : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Service>> GetAsync(Guid id)
    {
        var response = await _servicesService.GetByIdAsync(id);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        return Ok(response.Value);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateServiceRequest req)
    {
        await _unitOfWork.BeginTransactionAsync();

        var dto = new CreateServiceParams();

        var response = await _servicesService.CreateAsync(dto);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        await _unitOfWork.CommitAsync();

        return Ok(response);
    }
}
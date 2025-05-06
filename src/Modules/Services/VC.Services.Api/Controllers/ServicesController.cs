using FluentResults;
using VC.Services.Api.Models.Services;
using VC.Services.Application.ServicesUseCases;
using VC.Services.Application.ServicesUseCases.Models;

namespace VC.Services.Api.Controllers;

[Route("api/v1/services")]
public class ServicesController : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceDetailsDto>> GetAsync(Guid id,
        [FromServices] IGetServiceDetailsUseCase getServiceDetailsUseCase)
    {
        var serviceDetails = await getServiceDetailsUseCase.ExecuteAsync(id);
        if (serviceDetails is null)
            return NotFound();

        return Ok(serviceDetails);
    }

    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> CreateAsync(CreateServiceRequest req,
        [FromServices] ICreateServiceUseCase createServiceUseCase)
    {
        var dto = new CreateServiceParams(
            req.Title,
            req.BasePrice,
            req.BaseDuration,
            req.Description,
            req.CategoryId,
            req.RequiredResources,
            req.EmployeeAssignments?.Select(ea => new CreateServiceParams.EmployeeAssignmentDto()
            {
                EmployeeId = ea.EmployeeId,
                Duration = ea.Duration,
                Price = ea.Price,
            }).ToList());

        var result = await createServiceUseCase.ExecuteAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Result<Guid>>> UpdateAsync(Guid id, UpdateServiceRequest req,
        [FromServices] IUpdateServiceUseCase updateServiceUseCase)
    {
        var dto = new UpdateServiceParams(
            id,
            req.Title,
            req.BasePrice,
            req.BaseDuration,
            req.Description,
            req.CategoryId,
            req.RequiredResources,
            req.EmployeeAssignments?.Select(ea => new UpdateServiceParams.EmployeeAssignmentDto()
            {
                EmployeeId = ea.EmployeeId,
                Duration = ea.Duration,
                Price = ea.Price,
            }).ToList());

        var result = await updateServiceUseCase.ExecuteAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Result<Guid>>> DeleteAsync(Guid id,
        [FromServices] IRemoveServiceUseCase removeServiceUseCase)
    {
        var result = await removeServiceUseCase.ExecuteAsync(id);
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
}
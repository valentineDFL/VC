using Microsoft.AspNetCore.Mvc;
using VC.Services.Api.Models.Services;
using VC.Services.Application.ServicesUseCases;
using VC.Services.Application.ServicesUseCases.Models;

namespace VC.Services.Api.Controllers;

[Route("api/services")]
public class ServicesController : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id,
        [FromServices] IServiceDetailsQuery serviceDetailsQuery)
    {
        var serviceDetails = await serviceDetailsQuery.ExecuteAsync(id);
        if (serviceDetails is null)
            return NotFound();

        return Ok(serviceDetails);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateServiceRequest req,
        [FromServices] ICreateServiceCommand createServiceCommand)
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

        var response = await createServiceCommand.ExecuteAsync(dto);
        if (!response.IsSuccess)
            return new BadRequestObjectResult(new { Errors = response });

        return Ok(response);
    }
}
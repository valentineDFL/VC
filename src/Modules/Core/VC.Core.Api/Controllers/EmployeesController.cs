using FluentResults.Extensions.AspNetCore;
using VC.Core.Application;
using VC.Core.Application.EmployeesUseCases;
using VC.Core.Application.EmployeesUseCases.Models;

namespace VC.Core.Api.Controllers;

[Route("api/v1/employees")]
public class EmployeesController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(IGetEmployeesUseCase useCase)
    {
        return Ok(await useCase.ExecuteAsync(Unit.Value));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateEmployeeParams req, ICreateEmployeeUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(req);
        return result.ToActionResult();
    }
}
using FluentResults.Extensions.AspNetCore;
using VC.Core.Api.Models.WorkSchedules;
using VC.Core.Application.WorkScheduleUseCases;

namespace VC.Core.Api.Controllers;

[Route("api/v1/employees/{employeeId:guid}/work-schedule")]
public class EmployeeWorkSchedulesController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(
        Guid employeeId,
        [FromServices] IGetWorkScheduleDetailsUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(employeeId);
        return result.ToActionResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWorkScheduleAsync(
        Guid employeeId,
        [FromBody] CreateWorkScheduleRequest request,
        [FromServices] ICreateWorkScheduleUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(new CreateWorkScheduleParams(employeeId, request.Items));
        return result.ToActionResult();
    }
    
    [HttpPost("exceptions")]
    public async Task<IActionResult> AddWorkingHourExceptionAsync(
        Guid employeeId,
        [FromBody] AddWorkingHourExceptionRequest request,
        [FromServices] IAddWorkingHourExceptionUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(new AddWorkingHourExceptionParams(employeeId,
            request.Date,
            request.IsDayOff,
            request.StartTime,
            request.EndTime));
        return result.ToActionResult();
    }
}
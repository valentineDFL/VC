using VC.Core.Application.EmployeeAvailableSlotsUseCases;
using VC.Core.Application.EmployeeAvailableSlotsUseCases.Models;

namespace VC.Core.Api.Controllers;

[Route("api/v1/employees/{employeeId:guid}/available-slots")]
public class EmployeeAvailableSlotsController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(
        Guid employeeId,
        DateOnly date,
        IGetEmployeeAvailableSlotsUseCase useCase)
    {
        var availableSlots = await useCase.ExecuteAsync(new GetEmployeeAvailableSlotsParams(employeeId, date));

        if(!availableSlots.IsSuccess)
            return NotFound(availableSlots);

        return Ok(availableSlots.Value);
    }
}
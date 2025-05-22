using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using VC.Orders.Application;
using VC.Orders.Application.UseCases.Orders.Interfaces;

namespace VC.Orders.Api.Controllers;

[ApiController]
[Route("api/v1/payments")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class PaymentsController : ControllerBase
{
    [HttpPost("{orderId:guid}")]
    public async Task<ActionResult> GetAsync([FromKeyedServices(PayOrderUseCaseKeys.MockPayKey)] IPayOrderUseCase useCase, Guid orderId)
    {
        var result = await useCase.ExecuteAsync(orderId);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok();
    }
}
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using VC.Orders.Api.Dtos.Request;
using VC.Orders.Api.Filters;
using VC.Orders.Application;
using VC.Orders.Application.Dtos;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Application.UseCases.Payments.Interfaces;

namespace VC.Orders.Api.Controllers;

[ApiController]
[Route("api/v1/payments")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class PaymentsController : ControllerBase
{
    [HttpPost("{orderId:guid}")]
    [TypeFilter(typeof(IdempotencyActionFilter))]
    [TypeFilter(typeof(IdempotencyResultFilter))]
    public async Task<ActionResult<string>> MockPayOrderAsync(
                    [FromKeyedServices(PayOrderUseCaseKeys.MockPayKey)] IPayOrderUseCase useCase, 
                    Guid orderId, 
                    PayOrderRequest payOrderRequest, 
                    [FromHeader] string idempotencyKey)
    {
        var payOrderParams = payOrderRequest.Adapt<PayOrderParams>();

        var result = await useCase.ExecuteAsync(orderId, payOrderParams);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok();
    }

    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult<string>> GetStatusAsync([FromServices] IGetOrderPaymentStatusUseCase useCase, Guid orderId)
    {
        var result = await useCase.ExecuteAsync(orderId);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
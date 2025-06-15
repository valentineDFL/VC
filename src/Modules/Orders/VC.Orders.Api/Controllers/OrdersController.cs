using Mapster;
using Microsoft.AspNetCore.Mvc;
using VC.Orders.Api.Dtos.Request.Create;
using VC.Orders.Application.Dtos.Create;
using VC.Orders.Application.Dtos.Get;
using VC.Orders.Application.UseCases.Orders.Interfaces;

namespace VC.Orders.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class OrdersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDetailsDto>> GetByIdAsync([FromServices] IGetOrderUseCase useCase,
                                             Guid id,
                                             CancellationToken cts)
    {
        var order = await useCase.ExecuteAsync(id, cts);

        if(!order.IsSuccess)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromServices] ICreateOrderUseCase useCase,
                                                [FromBody] CreateOrderRequest orderRequest, 
                                                CancellationToken cts)
    {
        var dto = orderRequest.Adapt<CreateOrderParams>();

        var result = await useCase.ExecuteAsync(dto, cts);

        if (!result.IsSuccess)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<ActionResult> CancelAsync([FromServices] ICancelOrderUseCase useCase,
                                                Guid id)
    {
        var result = await useCase.ExecuteAsync(id);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
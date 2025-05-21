using Mapster;
using Microsoft.AspNetCore.Mvc;
using VC.Orders.Api.Dtos.Request.Create;
using VC.Orders.Application.Dtos.Create;
using VC.Orders.Application.UseCases.Orders.Interfaces;

namespace VC.Orders.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
[ApiExplorerSettings(GroupName = OpenApi.OpenApiConfig.GroupName)]
public class OrdersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetAsync([FromQuery] Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromServices] ICreateOrderUseCase useCase, CreateOrderRequest orderRequest, CancellationToken cts)
    {
        var dto = orderRequest.Adapt<CreateOrderParams>();

        var result = await useCase.ExecuteAsync(dto, cts);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> PayOrderAsync()
    {
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync([FromQuery] Guid id)
    {
        return Ok();
    }
}
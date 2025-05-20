using Microsoft.AspNetCore.Mvc;
using VC.Orders.Api.Dtos.Request.Create;

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

    // TODO: Перенести этот метод в контроллер для Ордер статусов
    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult> GetStatusesAsync([FromQuery] Guid orderId)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateOrderRequest orderRequest)
    {
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> ChangeStateAsync([FromQuery] Guid id)
    {
        return Ok();
    }
}
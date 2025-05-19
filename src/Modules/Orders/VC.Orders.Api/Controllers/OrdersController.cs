using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> CreateAsync()
    {
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> ChangeStateAsync([FromQuery] Guid id)
    {
        return Ok();
    }
}
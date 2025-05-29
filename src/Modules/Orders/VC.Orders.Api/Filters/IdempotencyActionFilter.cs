using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VC.Orders.Orders;
using VC.Orders.Repositories;
using VC.Shared.Utilities;

namespace VC.Orders.Api.Filters;

internal class IdempotencyActionFilter : Attribute, IAsyncActionFilter
{
    public const string IdempotencyKey = "idempotencyKey";
    public const string OrderId = "orderId";

    private readonly IUnitOfWork _unitOfWork;

    public IdempotencyActionFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        var key = httpContext.Request.Headers[IdempotencyKey];

        var cacheIdempotency = await _unitOfWork.OrdersIdempotenciesCache.GetByKeyAsync(key.ToString());

        if(cacheIdempotency is not null)
        {
            context.Result = GenerateObjectResult(StatusCodes.Status409Conflict, "Operation is Running");
            return;
        }

        var orderIdString = (string)httpContext.Request.RouteValues[OrderId];
        var orderId = Guid.Parse(orderIdString);

        var idempotency = await _unitOfWork.OrdersIdempotencies.GetByKeyAsync(key.ToString());

        var validationResult = await CheckIdempotencyValidAsync(idempotency, orderId);

        if (!validationResult.IsValid)
        {
            context.Result = validationResult.Error;
            return;
        }

        await _unitOfWork.BeginTransactionAsync();

        idempotency.ChangeStateToUsed();
        await _unitOfWork.OrdersIdempotenciesCache.AddAsync(idempotency);

        await _unitOfWork.CommitAsync();

        await next.Invoke();
    }

    private async Task<(bool IsValid, ContentResult? Error)> CheckIdempotencyValidAsync(OrderIdempotency idempotency, Guid orderId)
    {
        if (idempotency is null)
            return (false, GenerateContentResult(StatusCodes.Status404NotFound, "Idempotency Not Found"));

        if (idempotency.OrderId != orderId)
            return (false, GenerateContentResult(StatusCodes.Status409Conflict, "Key orderId and Request OrderId does not equals"));

        if (idempotency.Status == IdempotencyStatus.Used)
            return (false, GenerateContentResult(StatusCodes.Status409Conflict, $"Idempotency Key is {IdempotencyStatus.Used}"));

        return (true, null);
    }

    private ContentResult GenerateContentResult(int statusCode, string content)
        => new ContentResult { StatusCode = statusCode, Content = content };

    private ObjectResult GenerateObjectResult(int statusCode, object content)
        => new ObjectResult(content) { StatusCode = statusCode };
}
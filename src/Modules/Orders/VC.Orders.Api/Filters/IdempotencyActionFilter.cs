using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VC.Orders.Repositories;
using VC.Shared.Utilities;

namespace VC.Orders.Api.Filters;

internal class IdempotencyActionFilter : Attribute, IAsyncActionFilter
{
    public const string IdempotencyKey = "idempotencyKey";

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
            context.Result = new ContentResult { StatusCode = StatusCodes.Status409Conflict, Content = "Operation is Running" };
            return;
        }

        var idempotency = await _unitOfWork.OrdersIdempotencies.GetByKeyAsync(key.ToString());

        if(idempotency is null)
        {
            context.Result = new ContentResult { StatusCode = StatusCodes.Status404NotFound, Content = "Idempotency Not Found" };
            return;
        }

        if(idempotency.Status == IdempotencyStatus.Used)
        {
            context.Result = new ContentResult { StatusCode = StatusCodes.Status409Conflict, Content = $"Idempotency Key is {IdempotencyStatus.Used}" };
            return;
        }

        await _unitOfWork.BeginTransactionAsync();

        idempotency.ChangeStateToUsed();
        await _unitOfWork.OrdersIdempotenciesCache.AddAsync(idempotency);

        await _unitOfWork.CommitAsync();

        await next.Invoke();
    }
}
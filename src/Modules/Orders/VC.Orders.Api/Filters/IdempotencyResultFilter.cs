using Microsoft.AspNetCore.Mvc.Filters;
using VC.Orders.Application;
using VC.Orders.Orders;
using VC.Orders.Repositories;
using VC.Shared.Utilities;

namespace VC.Orders.Api.Filters;

internal class IdempotencyResultFilter : Attribute, IAsyncResultFilter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdempodencyKeyGenerator _keyGenerator;

    public IdempotencyResultFilter(IUnitOfWork unitOfWork, IIdempodencyKeyGenerator keyGenerator)
    {
        _unitOfWork = unitOfWork;
        _keyGenerator = keyGenerator;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        var orderIdString = (string)httpContext.Request.RouteValues[IdempotencyActionFilter.OrderId];

        var orderId = Guid.Parse(orderIdString);

        var id = Guid.CreateVersion7();
        var key = _keyGenerator.Generate();

        var idempotency = new OrderIdempotency(id, orderId, key);

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.OrdersIdempotencies.AddAsync(idempotency);
        await _unitOfWork.CommitAsync();

        httpContext.Response.Headers.Add(IdempotencyKey.Key, key);

        await next.Invoke();
    }
}
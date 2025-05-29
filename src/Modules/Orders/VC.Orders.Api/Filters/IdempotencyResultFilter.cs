using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using VC.Orders.Application;
using VC.Orders.Orders;
using VC.Orders.Repositories;

namespace VC.Orders.Api.Filters;

internal class IdempotencyResultFilter : Attribute, IAsyncResultFilter
{
    public const string OrderId = "orderId";

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

        var orderIdString = (string)httpContext.Request.RouteValues[OrderId];

        var orderId = Guid.Parse(orderIdString);

        var id = Guid.CreateVersion7();
        var key = _keyGenerator.Generate();

        var idempotency = new OrderIdempotency(id, orderId, key);

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.OrdersIdempotencies.AddAsync(idempotency);
        await _unitOfWork.CommitAsync();

        var resultType = context.Result.GetType();

        if(context.Result is ObjectResult contentResult)
        {
            if (contentResult.StatusCode == (int)HttpStatusCode.OK)
                context.Result = new ObjectResult(key) { StatusCode = StatusCodes.Status200OK };
        }

        await next.Invoke();
    }
}
using FluentResults;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;
using VC.Shared.Utilities;
using VC.Orders.Orders;

namespace VC.Orders.Application.UseCases.Orders;

internal class CancelOrderUseCase : ICancelOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(Guid orderId, CancellationToken cts = default)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order == null)
            return Result.Fail("Order Not Found");

        await _unitOfWork.BeginTransactionAsync();

        order.CancelOrder();
        await _unitOfWork.CommitAsync();

        return Result.Ok();
    }
}
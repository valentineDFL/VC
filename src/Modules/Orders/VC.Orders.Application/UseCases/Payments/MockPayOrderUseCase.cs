using FluentResults;
using VC.Orders.Application.Dtos;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;

namespace VC.Orders.Application.UseCases.Payments;

internal class MockPayOrderUseCase : IPayOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public MockPayOrderUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(Guid orderId, PayOrderParams @params)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order is null)
            return Result.Fail("Order Not Found");

        await _unitOfWork.BeginTransactionAsync();

        var result = order.ApplyOrderPayment();

        if (!result.IsSuccess)
        {
            await _unitOfWork.RollbackAsync();
            return Result.Fail(result.Errors);
        }

        await _unitOfWork.CommitAsync();

        return Result.Ok();
    }
}
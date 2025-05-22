using FluentResults;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;

namespace VC.Orders.Application.UseCases.Orders;

internal class PaypalPayOrderUseCase : IPayOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public PaypalPayOrderUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(Guid orderId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order is null)
            return Result.Fail("Order Not Found");

        

        throw new NotImplementedException();
    }
}
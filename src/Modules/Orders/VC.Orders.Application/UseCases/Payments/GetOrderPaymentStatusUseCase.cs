using FluentResults;
using VC.Orders.Application.UseCases.Payments.Interfaces;
using VC.Orders.Repositories;

namespace VC.Orders.Application.UseCases.Payments;

internal class GetOrderPaymentStatusUseCase : IGetOrderPaymentStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderPaymentStatusUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> ExecuteAsync(Guid orderId)
    {
        var payment = await _unitOfWork.Payments.GetByOrderIdAsync(orderId);

        if (payment is null)
            return Result.Fail("Payment not found");

        return Result.Ok(payment.State.ToString());
    }
}
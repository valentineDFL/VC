using FluentResults;

namespace VC.Orders.Application.UseCases.Payments.Interfaces;

public interface IGetOrderPaymentStatusUseCase
{
    public Task<Result<string>> ExecuteAsync(Guid orderId);
}
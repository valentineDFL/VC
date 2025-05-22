using FluentResults;

namespace VC.Orders.Application.UseCases.Orders.Interfaces;

public interface IPayOrderUseCase
{
    public Task<Result> ExecuteAsync(Guid orderId);
}
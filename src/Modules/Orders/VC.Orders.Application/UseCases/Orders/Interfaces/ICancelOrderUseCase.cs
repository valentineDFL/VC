using FluentResults;

namespace VC.Orders.Application.UseCases.Orders.Interfaces;

public interface ICancelOrderUseCase
{
    public Task<Result> ExecuteAsync(Guid orderId, CancellationToken cts = default);
}
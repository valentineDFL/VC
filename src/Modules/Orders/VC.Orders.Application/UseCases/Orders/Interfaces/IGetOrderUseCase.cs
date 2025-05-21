using FluentResults;
using VC.Orders.Application.Dtos.Get;

namespace VC.Orders.Application.UseCases.Orders.Interfaces;

public interface IGetOrderUseCase
{
    public Task<Result<OrderDetailsDto>> ExecuteAsync(Guid id, CancellationToken cts);
}
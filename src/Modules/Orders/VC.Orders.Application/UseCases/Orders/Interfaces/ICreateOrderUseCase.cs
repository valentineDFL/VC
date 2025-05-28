using FluentResults;
using VC.Orders.Application.Dtos.Create;

namespace VC.Orders.Application.UseCases.Orders.Interfaces;

public interface ICreateOrderUseCase
{
    public Task<Result<CreateOrderResponseParams>> ExecuteAsync(CreateOrderParams @params, CancellationToken cts);
}
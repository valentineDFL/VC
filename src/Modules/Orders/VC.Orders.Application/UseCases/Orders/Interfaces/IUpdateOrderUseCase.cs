using FluentResults;
using VC.Orders.Application.Dtos.Update;

namespace VC.Orders.Application.UseCases.Orders.Interfaces;

public interface IUpdateOrderUseCase
{
    public Task<Result> ExecuteAsync(UpdateOrderParams @params, CancellationToken cts);
}
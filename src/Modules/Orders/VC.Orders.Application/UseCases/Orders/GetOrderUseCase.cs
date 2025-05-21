using FluentResults;
using VC.Orders.Application.Dtos.Get;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;

namespace VC.Orders.Application.UseCases.Orders;

internal class GetOrderUseCase : IGetOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OrderDetailsDto>> ExecuteAsync(Guid id, CancellationToken cts)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id);

        if (order is null)
            return Result.Fail("Order not found");


        return Result.Fail("");
        //return Result.Ok(order);
    }

    //private async Task<string> 
}
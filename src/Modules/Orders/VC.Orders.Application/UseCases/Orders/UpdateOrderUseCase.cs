using FluentResults;
using VC.Orders.Application.Dtos.Update;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;

namespace VC.Orders.Application.UseCases.Orders;

internal class UpdateOrderUseCase : IUpdateOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(UpdateOrderParams @params, CancellationToken cts)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(@params.OrderId);

        if (order == null)
            return Result.Fail("Order Not Found");

        await _unitOfWork.BeginTransactionAsync();

        order.Update(@params.State, @params.EmployeeId, order.Price);
        await _unitOfWork.CommitAsync();

        return Result.Ok();
    }

    private async Task<string> GetEmployeeData()
    {


        return "";
    }
}
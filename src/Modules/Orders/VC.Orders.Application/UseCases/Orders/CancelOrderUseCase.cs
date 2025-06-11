using FluentResults;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;
using VC.Shared.RabbitMQIntegration.Publishers.Interfaces;
using VC.Shared.Utilities.RabbitEnums;

namespace VC.Orders.Application.UseCases.Orders;

internal class CancelOrderUseCase : ICancelOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CancelOrderUseCase(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> ExecuteAsync(Guid orderId, CancellationToken cts = default)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order == null)
            return Result.Fail("Order Not Found");

        await _unitOfWork.BeginTransactionAsync();

        var result = order.CancelOrder();
        if (!result.IsSuccess)
        {
            await _unitOfWork.RollbackAsync();
            return Result.Fail(result.Errors);
        }

        await _unitOfWork.CommitAsync();

        await _publisher.PublishAsync(order, Exchanges.ChangedOrdersDirect, RoutingKeys.ChangedOrdersKey, cts);

        return Result.Ok();
    }
}
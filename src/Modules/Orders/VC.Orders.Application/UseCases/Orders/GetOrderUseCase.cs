using FluentResults;
using Mapster;
using VC.Orders.Application.Dtos.Get;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;
using VC.Shared.Utilities.ApiClient;

namespace VC.Orders.Application.UseCases.Orders;

internal class GetOrderUseCase : IGetOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICoreServiceApiClient _serviceClient;

    public GetOrderUseCase(IUnitOfWork unitOfWork, ICoreServiceApiClient serviceClient)
    {
        _unitOfWork = unitOfWork;
        _serviceClient = serviceClient;
    }

    public async Task<Result<OrderDetailsDto>> ExecuteAsync(Guid orderId, CancellationToken cts = default)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order is null)
            return Result.Fail("Order not found");

        var service = await GetServiceDataAsync(order.ServiceId);

        if (!service.IsSuccess)
            return Result.Fail(service.Errors);

        var orderDetails = new OrderDetailsDto(orderId, service.Value, order.Price, order.ServiceTime);

        return Result.Ok(orderDetails);
    }

    private async Task<Result<ServiceDetailDto>> GetServiceDataAsync(Guid serviceId, CancellationToken cts = default)
    {
        var response = await _serviceClient.GetServiceAsync(serviceId, cts);

        if (!response.IsSuccess)
            return Result.Fail(response.Errors);

        var serviceDto = response.Value.Adapt<ServiceDetailDto>();

        return Result.Ok(serviceDto);
    }
}
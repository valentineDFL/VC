using FluentResults;
using Microsoft.Extensions.Options;
using VC.Orders.Application.Dtos.Get;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Repositories;
using VC.Shared.Utilities;
using VC.Shared.Utilities.Options.Uris;

namespace VC.Orders.Application.UseCases.Orders;

internal class GetOrderUseCase : IGetOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly CoreModuleControllers _coreModuleControllers;
    private readonly HttpApiClient _httpApiClient;

    public GetOrderUseCase(IUnitOfWork unitOfWork, IOptions<CoreModuleControllers> options, HttpApiClient httpApiClient)
    {
        _unitOfWork = unitOfWork;
        _httpApiClient = httpApiClient;
        _coreModuleControllers = options.Value;
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
        var uri = _coreModuleControllers.Service.ParametrizedGetRequestUri(serviceId);

        var response = await _httpApiClient.GetAsEntityAsync<ServiceDetailDto>(uri, cts);

        if (!response.IsSuccess)
            return Result.Fail(response.Errors);

        return Result.Ok(response.Value);
    }
}
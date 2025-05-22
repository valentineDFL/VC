using FluentResults;
using Microsoft.Extensions.Options;
using VC.Orders.Application.Dtos.Create;
using VC.Orders.Application.Dtos.OtherModules;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Orders;
using VC.Orders.Payments;
using VC.Orders.Repositories;
using VC.Shared.Utilities;
using VC.Shared.Utilities.Options.Uris;

namespace VC.Orders.Application.UseCases.Orders;

internal class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CoreModuleControllers _coreModuleControllers;
    private readonly HttpApiClient _httpApiClient;

    public CreateOrderUseCase(IUnitOfWork unitOfWork, IOptions<CoreModuleControllers> options, HttpApiClient httpApiClient)
    {
        _unitOfWork = unitOfWork;
        _coreModuleControllers = options.Value;
        _httpApiClient = httpApiClient;
    }

    public async Task<Result<Guid>> ExecuteAsync(CreateOrderParams @params, CancellationToken cts)
    {
        var orderId = Guid.CreateVersion7();

        var payment = new Payment(Guid.CreateVersion7(), orderId, null);

        var serviceResult = await GetServiceData(@params.ServiceId, cts);

        if (!serviceResult.IsSuccess)
            return Result.Fail(serviceResult.Errors);

        var serviceData = serviceResult.Value;
        var employee = serviceData.EmployeeAssignments.FirstOrDefault(e => e.EmployeeId == @params.EmployeeId);

        if (employee is null)
            throw new NullReferenceException("Employee does not exists");

        var order = new Order(orderId, @params.ServiceTime, employee.Price, @params.ServiceId, @params.EmployeeId, null);

        await _unitOfWork.BeginTransactionAsync(cts);

        await _unitOfWork.Orders.CreateAsync(order);
        await _unitOfWork.Payments.CreateAsync(payment);

        await _unitOfWork.CommitAsync(cts);

        return Result.Ok(orderId);
    }

    private async Task<Result<ServiceDetailsDto>> GetServiceData(Guid serviceId, CancellationToken cts)
    {
        var uri = _coreModuleControllers.Service.ParametrizedGetRequestUri(serviceId);

        var result = await _httpApiClient.GetAsEntityAsync<ServiceDetailsDto>(uri, cts);

        if(!result.IsSuccess)
            return Result.Fail(result.Errors);

        return Result.Ok(result.Value);
    }
}
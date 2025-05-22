using FluentResults;
using VC.Orders.Application.Dtos.Create;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Orders;
using VC.Orders.Payments;
using VC.Orders.Repositories;
using VC.Shared.Utilities.ApiClient;
using VC.Shared.Utilities.CoreModuleDtos;

namespace VC.Orders.Application.UseCases.Orders;

internal class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICoreServiceApiClient _serviceApiClient;

    public CreateOrderUseCase(IUnitOfWork unitOfWork, ICoreServiceApiClient serviceClient)
    {
        _unitOfWork = unitOfWork;
        _serviceApiClient = serviceClient;
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
        var result = await _serviceApiClient.GetServiceAsync(serviceId, cts);

        if(!result.IsSuccess)
            return Result.Fail(result.Errors);

        return result;
    }
}
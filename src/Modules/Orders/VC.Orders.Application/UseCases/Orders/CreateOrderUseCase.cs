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
    private readonly IIdempodencyKeyGenerator _keyGenerator;

    public CreateOrderUseCase(IUnitOfWork unitOfWork, ICoreServiceApiClient serviceClient, IIdempodencyKeyGenerator keyGenerator)
    {
        _unitOfWork = unitOfWork;
        _serviceApiClient = serviceClient;
        _keyGenerator = keyGenerator;
    }

    public async Task<Result<CreateOrderResponseParams>> ExecuteAsync(CreateOrderParams @params, CancellationToken cts)
    {
        var orderId = Guid.CreateVersion7();

        var payment = new Payment(Guid.CreateVersion7(), orderId, null);

        var serviceResult = await GetServiceData(@params.ServiceId, cts);

        if (!serviceResult.IsSuccess)
            return Result.Fail(serviceResult.Errors);

        var serviceData = serviceResult.Value;
        var employee = serviceData.EmployeeAssignments.FirstOrDefault(e => e.EmployeeId == @params.EmployeeId);

        if (employee is null)
            return Result.Fail("Employee Not Found");

        var order = new Order(orderId, @params.ServiceTime, employee.Price, @params.ServiceId, @params.EmployeeId, null);

        var idempodency = new OrderIdempotency(Guid.CreateVersion7(), orderId, _keyGenerator.Generate());

        await _unitOfWork.BeginTransactionAsync(cts);

        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.Payments.AddAsync(payment);

        await _unitOfWork.OrdersIdempotencies.AddAsync(idempodency);

        await _unitOfWork.CommitAsync(cts);

        var result = new CreateOrderResponseParams(orderId, idempodency.Key);

        return Result.Ok(result);
    }

    private async Task<Result<ServiceDetailsDto>> GetServiceData(Guid serviceId, CancellationToken cts = default)
    {
        var result = await _serviceApiClient.GetServiceAsync(serviceId, cts);

        if(!result.IsSuccess)
            return Result.Fail(result.Errors);

        return result;
    }
}
using FluentResults;
using VC.Orders.Application.Dtos.Create;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Orders;
using VC.Orders.Payments;
using VC.Orders.Repositories;
using VC.Shared.RabbitMQIntegration.Publishers.Interfaces;
using VC.Shared.Utilities.ApiClient.CoreModule.Interfaces;
using VC.Shared.Utilities.RabbitEnums;

namespace VC.Orders.Application.UseCases.Orders;

internal class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdempodencyKeyGenerator _keyGenerator;

    private readonly ICoreServicesApiClient _serviceApiClient;
    private readonly ICoreAvailableSlotsApiClient _availableSlotsApiClient;

    private readonly IPublisher _publisher;

    public CreateOrderUseCase(IUnitOfWork unitOfWork, 
                              ICoreServicesApiClient serviceClient, 
                              ICoreAvailableSlotsApiClient availableSlotsApiClient,
                              IIdempodencyKeyGenerator keyGenerator,
                              IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _keyGenerator = keyGenerator;

        _serviceApiClient = serviceClient;
        _availableSlotsApiClient = availableSlotsApiClient;

        _publisher = publisher;
    }

    public async Task<Result<CreateOrderResponseParams>> ExecuteAsync(CreateOrderParams @params, CancellationToken cts)
    {
        var validateResult = await ValidateOrderTimeSlotIsReservedAsync(@params.EmployeeId, @params.ServiceTime, cts);
        if (!validateResult.IsSuccess)
            return Result.Fail(validateResult.Errors);

        var serviceResult = await _serviceApiClient.GetServiceAsync(@params.ServiceId, cts);

        if (!serviceResult.IsSuccess)
            return Result.Fail(serviceResult.Errors);

        var employee = serviceResult
            .Value
            .EmployeeAssignments
            .FirstOrDefault(e => e.EmployeeId == @params.EmployeeId);

        if (employee is null)
            return Result.Fail("Employee Not Found");

        var orderId = Guid.CreateVersion7();
        var payment = new Payment(Guid.CreateVersion7(), orderId, null);
        var order = new Order(orderId, @params.ServiceTime, employee.Price, @params.ServiceId, @params.EmployeeId, null);
        var idempotency = new OrderIdempotency(Guid.CreateVersion7(), orderId, _keyGenerator.Generate());

        await AddOrderThingsToDbAsync(order, payment, idempotency, cts);

        await _publisher.PublishAsync(order, Exchanges.OrdersDirect, RoutingKeys.OrdersKey, Queues.Orders);

        var result = new CreateOrderResponseParams(orderId, idempotency.Key);

        return Result.Ok(result);
    }

    private async Task<Result> ValidateOrderTimeSlotIsReservedAsync(Guid employeeId, DateTime serviceTime, CancellationToken cts = default)
    {
        var availableSlotsResult = await _availableSlotsApiClient.GetAvailableSlotsAsync(employeeId, DateOnly.FromDateTime(serviceTime), cts);
        if (!availableSlotsResult.IsSuccess)
            Result.Fail(availableSlotsResult.Errors);

        var availableSlots = availableSlotsResult.Value;

        if (availableSlots.Any(avs => avs.From == serviceTime))
            return Result.Ok();

        return Result.Fail("Slot non available");
    }

    private async Task AddOrderThingsToDbAsync(Order order, Payment payment, OrderIdempotency idempotency, CancellationToken cts = default)
    {
        await _unitOfWork.BeginTransactionAsync(cts);

        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.Payments.AddAsync(payment);

        await _unitOfWork.OrdersIdempotencies.AddAsync(idempotency);

        await _unitOfWork.CommitAsync(cts);
    }
}
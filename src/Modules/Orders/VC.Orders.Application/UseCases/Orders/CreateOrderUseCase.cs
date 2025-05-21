using FluentResults;
using System.Text.Json;
using VC.Orders.Application.Dtos.Create;
using VC.Orders.Application.Dtos.OtherModules;
using VC.Orders.Application.UseCases.Orders.Interfaces;
using VC.Orders.Orders;
using VC.Orders.Payments;
using VC.Orders.Repositories;

namespace VC.Orders.Application.UseCases.Orders;

internal class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> ExecuteAsync(CreateOrderParams @params, CancellationToken cts)
    {
        var orderId = Guid.CreateVersion7();

        var payment = new Payment(Guid.CreateVersion7(), orderId, null);

        var serviceData = await GetServiceData("http://localhost:5056/api/v1/services/", @params.ServiceId, cts);

        if (!serviceData.IsSuccess)
            return Result.Fail(serviceData.Errors);

        var options = new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;

        var dto = JsonSerializer.Deserialize<ServiceDetailsDto>(serviceData.Value, options);
        var orderPrice = CalculateOrderPrice(dto.BasePrice, dto.EmployeeAssignments);

        var order = new Order(orderId, orderPrice, @params.ServiceId, @params.EmployeeId, null);

        await _unitOfWork.BeginTransactionAsync(cts);

        await _unitOfWork.Orders.CreateAsync(order);
        await _unitOfWork.Payments.CreateAsync(payment);

        await _unitOfWork.CommitAsync(cts);

        return Result.Ok(orderId);
    }

    private async Task<Result<string>> GetServiceData(string serviceUrl, Guid serviceId, CancellationToken cts)
    {
        var client = new HttpClient();

        var uri = $"{serviceUrl}{serviceId}";

        using var response = await client.GetAsync(uri, cts);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return Result.Fail("Service Not Found");

        var json = await response.Content.ReadAsStringAsync();

        return Result.Ok(json);
    }

    private decimal CalculateOrderPrice(decimal servicePrice, List<EmployeeAssignmentDto> employees)
        => employees.Sum(e => e.Price) + servicePrice;
}
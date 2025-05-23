using FluentResults;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Application.Abstractions;

public interface IUserOrderService
{
    Task<Result> CreateUserOrderAsync(CreateOrderRequest request);

    Task<Result> CancelOrderAsync(Guid id, Guid userId);

    Task<Result> GetAvailableSlotsAsync(Guid serviceId, Guid userId, DateTime fromDate, DateTime toDate);
}
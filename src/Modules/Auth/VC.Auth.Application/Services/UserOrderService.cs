using FluentResults;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Application.Services;

//TODO: реализовать UserOrderService
public class UserOrderService : IUserOrderService
{
    public Task<Result> CreateUserOrderAsync(CreateOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result> CancelOrderAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetAvailableSlotsAsync(Guid serviceId, Guid userId, DateTime fromDate, DateTime toDate)
    {
        throw new NotImplementedException();
    }
}
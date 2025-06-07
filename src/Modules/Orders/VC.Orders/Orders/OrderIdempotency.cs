using FluentResults;
using System.Text;
using VC.Orders.Common;
using VC.Shared.Utilities;

namespace VC.Orders.Orders;

public class OrderIdempotency : AggregateRoot<Guid>
{
    public const int KeyLength = 32;

    public OrderIdempotency(Guid id, Guid orderId, string key) : base(id)
    {
        var errors = new StringBuilder();

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (orderId == Guid.Empty)
            errors.AppendLine("OrderId cannot be empty");

        if (id == orderId)
            errors.AppendLine("Id and orderId cannot be equals");

        if (string.IsNullOrEmpty(key))
            errors.AppendLine("Key cannot be empty");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        OrderId = orderId;
        Key = key;
        Status = IdempotencyStatus.Available;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid OrderId { get; private set; }

    public string Key { get; private set; }

    public IdempotencyStatus Status { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public Result ChangeStateToUsed()
    {
        if (Status == IdempotencyStatus.Used)
            return Result.Fail($"State is {Status} now");

        Status = IdempotencyStatus.Used;

        return Result.Ok();
    }
}
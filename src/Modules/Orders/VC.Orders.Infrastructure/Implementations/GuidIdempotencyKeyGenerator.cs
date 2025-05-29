using VC.Orders.Application;
using VC.Orders.Orders;

namespace VC.Orders.Infrastructure.Implementations;

internal class GuidIdempotencyKeyGenerator : IIdempodencyKeyGenerator
{
    public string Generate()
        => Guid.CreateVersion7().ToString()[..OrderIdempotency.KeyLength];
}
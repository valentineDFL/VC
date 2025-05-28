using VC.Orders.Application;
using VC.Orders.Orders;

namespace VC.Orders.Infrastructure.Implementations;

internal class GuidIdempodencyKeyGenerator : IIdempodencyKeyGenerator
{
    public string Generate()
    {
        var guid = Guid.CreateVersion7().ToString();

        return guid.Substring(0, OrderIdempotency.KeyLength);
    }
}
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VC.Orders.Orders;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class RedisCacheOrdersIdempotenciesRepository : IOrdersIdempotenciesRepository
{
    private readonly IDistributedCache _cache;

    public RedisCacheOrdersIdempotenciesRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<OrderIdempotency> GetByKeyAsync(string key)
    {
        var orderIdempotencyString = await _cache.GetStringAsync(key);

        if (orderIdempotencyString is null)
            return null;

        var result = JsonSerializer.Deserialize<OrderIdempotency>(orderIdempotencyString);

        return result;
    }

    public async Task AddAsync(OrderIdempotency orderIdempotency)
    {
        var key = orderIdempotency.Key;
        var orderIdempotencyString = JsonSerializer.Serialize(orderIdempotency);

        await _cache.SetStringAsync(key, orderIdempotencyString, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(IUnitOfWork.CacheLifetimeFromMinutes)
        });
    }

    public async Task UpdateAsync(OrderIdempotency orderIdempotency)
    {
        var key = orderIdempotency.Key;
        var orderIdempotencyString = JsonSerializer.Serialize(orderIdempotency);

        await _cache.SetStringAsync(key, orderIdempotencyString, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(IUnitOfWork.CacheLifetimeFromMinutes)
        });
    }
}
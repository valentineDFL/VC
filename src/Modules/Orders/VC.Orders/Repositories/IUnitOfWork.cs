namespace VC.Orders.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    public const string PostgresOrdersKey = nameof(PostgresOrdersKey);
    public const string RedisCacheOrdersKey = nameof(RedisCacheOrdersKey);

    public const int CacheLifetimeFromMinutes = 3;

    public IOrdersRepository Orders { get; }

    public IPaymentsRepository Payments { get; }

    public IOutboxMessagesRepository OutboxMessages { get; }

    public IOrdersIdempotenciesRepository OrdersIdempotencies { get; }

    public IOrdersIdempotenciesRepository OrdersIdempotenciesCache { get; }

    public Task BeginTransactionAsync(CancellationToken cts = default);

    public Task CommitAsync(CancellationToken cts = default);

    public Task RollbackAsync(CancellationToken cts = default);
}
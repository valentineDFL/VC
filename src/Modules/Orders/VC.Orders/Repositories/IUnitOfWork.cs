namespace VC.Orders.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    public IOrdersRepository Orders { get; }

    public IPaymentsRepository Payments { get; }

    public IOutboxMessagesRepository OutboxMessages { get; }

    public Task BeginTransactionAsync(CancellationToken cts = default);

    public Task CommitAsync(CancellationToken cts = default);

    public Task RollbackAsync(CancellationToken cts = default);
}
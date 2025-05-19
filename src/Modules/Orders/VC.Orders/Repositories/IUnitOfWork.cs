namespace VC.Orders.Repositories;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    public IOrderRepository OrderRepository { get; }

    public IPaymentsRepository PaymentsRepository { get; }

    public Task StartTransactionAsync();

    public Task RollbackAsync();

    public Task CommitAsync();
}
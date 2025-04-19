namespace VC.Utilities;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();

    Task RollbackTransactionAsync();

    Task CommitTransactionAsync();
}
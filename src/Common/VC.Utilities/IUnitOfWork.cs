namespace VC.Utilities;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();

    Task BeginTransactionAsync();

    Task RollbackAsync();
}
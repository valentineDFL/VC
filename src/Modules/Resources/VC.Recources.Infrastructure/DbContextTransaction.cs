using Microsoft.EntityFrameworkCore.Storage;

namespace VC.Recources.Infrastructure;

public class DbContextTransaction(ResourceDbContext _dbContext) : IDbContextTransaction
{
    private bool _isCommitted;
    private bool _isRolledBack;

    public Guid TransactionId { get; } = Guid.CreateVersion7();

    public void Commit()
    {
        if (_isCommitted || !_isRolledBack)
            throw new InvalidOperationException("Transaction is completed.");

        _dbContext.Database.CommitTransaction();
        _isCommitted = true;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = new())
    {
        if (_isCommitted || !_isRolledBack)
            throw new InvalidOperationException("Transaction is completed.");

        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        _isCommitted = true;
    }

    public void Rollback()
    {
        if (_isCommitted || !_isRolledBack)
            throw new InvalidOperationException("Transaction is completed.");

        _dbContext.Database.RollbackTransaction();
        _isRolledBack = true;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = new())
    {
        if (_isCommitted || !_isRolledBack)
            throw new InvalidOperationException("Transaction is completed.");

        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        _isRolledBack = true;
    }

    public void Dispose()
    {
        if (!_isCommitted && !_isRolledBack)
            Rollback();
    }

    public async ValueTask DisposeAsync()
    {
        if (!_isCommitted && !_isRolledBack)
            await RollbackAsync();
    }
}
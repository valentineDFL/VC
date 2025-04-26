using Microsoft.EntityFrameworkCore.Storage;
using VC.Utilities;

namespace VC.Recources.Infrastructure;

internal class UnitOfWork(ResourceDbContext _dbContext) : IUnitOfWork, IDisposable
{
    private IDbContextTransaction _transaction;
    private bool _isCompleted;

    public void BeginTransaction()
    {
        if (_transaction is not null)
            throw new InvalidOperationException("Transaction is started");

        _transaction = _dbContext.Database.BeginTransaction();
        _isCompleted = false;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null)
            throw new InvalidOperationException("Transaction is started");

        _transaction = await _dbContext.Database.BeginTransactionAsync();
        _isCompleted = false;
    }

    public void Commit()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        _dbContext.SaveChanges();
        _transaction.Commit();
        _isCompleted = true;
    }

    public async Task CommitAsync()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        await _dbContext.SaveChangesAsync();
        await _transaction.CommitAsync();
        _isCompleted = true;
    }

    public void Rollback()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        _transaction.Rollback();
    }

    public async Task RollbackAsync()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        await _transaction.RollbackAsync();
    }

    public void SaveChanges() => _dbContext.SaveChanges();

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public void Dispose()
    {
        if (!_isCompleted)
        {
            Rollback();
        }

        _transaction.Dispose();
        _dbContext.Dispose();
    }
}
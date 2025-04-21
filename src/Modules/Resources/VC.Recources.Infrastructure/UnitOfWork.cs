using Microsoft.EntityFrameworkCore.Storage;
using VC.Utilities;

namespace VC.Recources.Infrastructure;

internal class UnitOfWork(ResourceDbContext _dbContext) : IUnitOfWork, IDisposable
{
    private IDbContextTransaction _transaction;

    public void BeginTransaction()
    {
        if (_transaction is not null)
            throw new InvalidOperationException("Transaction not started");

        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        _dbContext.SaveChanges();
        _transaction.Commit();
    }

    public void Rollback()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started");

        _transaction.Rollback();
    }

    public void SaveChanges() => _dbContext.SaveChanges();

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }
}
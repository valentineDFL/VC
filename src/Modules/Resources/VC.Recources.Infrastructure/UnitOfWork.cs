using VC.Recources.Domain;
using VC.Recources.Domain.UnitOfWork;

namespace VC.Recources.Infrastructure;

public class UnitOfWork(DbContext.DbContext _dbContext) : IResourcesUnitOfWork
{
    public IRepository Resources { get; }

    public async Task RollbackTransactionAsync()
        => await _dbContext.Database.RollbackTransactionAsync();

    /// <summary>
    /// Save changes and commit transaction
    /// </summary>
    public async Task CommitTransactionAsync()
    {
        await _dbContext.SaveChangesAsync();
        await _dbContext.Database.CommitTransactionAsync();
    }

    public async Task BeginTransactionAsync()
        => await _dbContext.Database.BeginTransactionAsync();

    public void Dispose()
        => _dbContext.Dispose();
}
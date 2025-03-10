using Microsoft.EntityFrameworkCore;
using VC.Tenants.Repositories;

namespace VC.Tenants.Infrastructure.Persistence.Repositories;

/// <summary>
/// Базовый репозиторий.
/// </summary>
/// <typeparam name="TEntity">Доменная сущность.</typeparam>
internal class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> Query;
    protected readonly DatabaseContext DbContext;

    public RepositoryBase(DatabaseContext dbContext)
    {
        DbContext = dbContext;
        Query = DbContext.Set<TEntity>();
    }
}
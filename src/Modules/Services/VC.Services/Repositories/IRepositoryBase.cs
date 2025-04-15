namespace VC.Services.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    public Task Remove(TEntity entity, CancellationToken cancellationToken);
    public Task Update(TEntity entity, CancellationToken cancellationToken);
}

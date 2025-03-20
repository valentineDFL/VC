using VC.Recources.UnitOfWork;

namespace VC.Recources.Infrastructure;

internal class DbSaver : IDbSaver
{
    private readonly ResourceDbContext _dbContext;

    public DbSaver(ResourceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

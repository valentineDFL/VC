using VC.Recources.Domain.UnitOfWork;

namespace VC.Recources.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ResourceDbContext _dbContext;
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
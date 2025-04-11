using Microsoft.EntityFrameworkCore;
using VC.Recources.Domain;

namespace VC.Recources.Infrastructure;

public class NameUniquenessChecker : INameUniquenessChecker
{
    private readonly ResourceDbContext _dbContext;

    public NameUniquenessChecker(ResourceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsNameUniqueAsync(string name, Guid currentId)
    {
        return !await _dbContext.Resources
            .AnyAsync(r => r.Name == name && r.Id != currentId);
    }
}
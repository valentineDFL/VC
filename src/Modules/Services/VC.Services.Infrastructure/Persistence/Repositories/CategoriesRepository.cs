using VC.Services.Repositories;

namespace VC.Services.Infrastructure.Persistence.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
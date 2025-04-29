namespace VC.Services.Repositories;

public interface ICategoriesRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
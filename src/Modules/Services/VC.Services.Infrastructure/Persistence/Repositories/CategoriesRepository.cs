using VC.Services.Repositories;

namespace VC.Services.Infrastructure.Persistence.Repositories;

public class CategoriesRepository : BaseRepository<Category, Guid>, ICategoriesRepository
{
    public CategoriesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
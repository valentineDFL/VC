using VC.Core.Repositories;
using VC.Core.Services;

namespace VC.Core.Infrastructure.Persistence.Repositories;

public class CategoriesRepository : BaseRepository<Category, Guid>, ICategoriesRepository
{
    public CategoriesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
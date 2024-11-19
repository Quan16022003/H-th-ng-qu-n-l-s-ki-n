using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class CategoryEventRepository(RepositoryDbContext dbContext)
     : BaseRepository<CategoryEvents>(dbContext), ICategoryEventRepository
    {
    }
}

using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class CategoryEventRepository(RepositoryDbContext dbContext)
     : BaseRepository<CategoryEvents>(dbContext), ICategoryEventRepository
    {
        public async Task<CategoryEvents?> GetBySlugAsync(string slug)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Slug == slug);
        }
    }
}

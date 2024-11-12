using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CategoryEventRepository : BaseRepository<CategoryEvents>, ICategoryEventRepository
    {
        public CategoryEventRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            References.Add(e => e.Events);
        }
    }
}

using Domain.Entities;
using Domain.Repositories;
using Services.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryEventService : ICategoryEventService
    {
        private readonly ICategoryEventRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryEventService(ICategoryEventRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryEvents>> GetAllCategoryEventsAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}

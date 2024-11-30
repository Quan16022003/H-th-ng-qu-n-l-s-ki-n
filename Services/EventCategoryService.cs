using Constracts.DTO;
using Constracts.EventCategory;
using Domain.Repositories;
using Services.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Domain.Entities;

namespace Services
{
    internal sealed class EventCategoryService : IEventCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryEventRepository _categoryEventRepository;
        private readonly IFileService _fileService;
        private readonly ISlugService _slugService;

        public EventCategoryService(
            IUnitOfWork unitOfWork, 
            IFileService fileService, 
            ISlugService slugService)
        {
            _unitOfWork = unitOfWork;
            _categoryEventRepository = _unitOfWork.CategoryEventRepository;
            _fileService = fileService;
            _slugService = slugService;
        }
        public async Task<EventCategoryDTO> CreateAsync(EventCategoryCreationDto createDto)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            CategoryEvents categoryEvent = new()
            {
                Name = createDto.Name, Description = createDto.Description, ThumbnailUrl = await _fileService.UploadFileAsync(createDto.ImageFile, "images/client"),
                Slug = _slugService.GenerateSlug(createDto.Name!),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = true
            };


            await _categoryEventRepository.AddAsync(categoryEvent);
            
            await _unitOfWork.CompleteAsync();

            // Sử dụng Mapster để chuyển đổi từ entity sang DTO
            return categoryEvent.Adapt<EventCategoryDTO>();
        }

        public async Task DeleteAsync(int id)
        {
            // Kiểm tra category có tồn tại không
            CategoryEvents? categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
            if (categoryEvent == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với id: {id}");
            }

            await _categoryEventRepository.SoftDeleteAsync(categoryEvent);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            CategoryEvents categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
            return !categoryEvent.IsDeleted;
        }

        public async Task<IEnumerable<EventCategoryDTO>> GetAllAsync()
        {
            // Lấy tất cả category chưa bị xóa mềm
            IEnumerable<CategoryEvents> categories = await _categoryEventRepository.GetAllAsync();
            categories = categories.Where(c => !c.IsDeleted).ToList();

            // Chuyển đổi sang DTO bằng Mapster
            return categories.Adapt<IEnumerable<EventCategoryDTO>>();
        }

        public async Task<EventCategoryDTO> GetByIdAsync(int id)
        {
            // Lấy category theo id
            var categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
            if (categoryEvent == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với id: {id}");
            }
            return categoryEvent.Adapt<EventCategoryDTO>();
        }

        public async Task<EventCategoryDTO> GetBySlugAsync(string slug)
        {
            var categoryEvent = await _categoryEventRepository.GetBySlugAsync(slug);
            if (categoryEvent == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với slug: {slug}");
            }
            return categoryEvent.Adapt<EventCategoryDTO>();
        }

        public async Task<EventCategoryDTO> UpdateAsync(int id, EventCategoryDTO updateDto)
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            // Kiểm tra category có tồn tại không
            var categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
            if (categoryEvent == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với id: {id}");
            }

            if (updateDto?.ImageFile != null)
            {
                categoryEvent.ThumbnailUrl = await _fileService.UploadFileAsync(updateDto.ImageFile, "images/client");
            }

            updateDto.Adapt(categoryEvent);
            categoryEvent.CreatedDate = categoryEvent.CreatedDate;
            categoryEvent.Slug = _slugService.GenerateSlug(updateDto.Name);
            categoryEvent.ModifiedDate = DateTime.Now;

            await _categoryEventRepository.UpdateAsync(categoryEvent);
            await _unitOfWork.CompleteAsync();

            return categoryEvent.Adapt<EventCategoryDTO>();
        }
        
        public bool IsCategoryNameUnique(string name)
        {
            return _categoryEventRepository.GetByName(name);
        }

        public bool IsCategoryInUse(int id)
        {
            return _categoryEventRepository.IsCategoryInUse(id);
        }
    }
}

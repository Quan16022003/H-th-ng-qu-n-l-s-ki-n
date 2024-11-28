using Constracts.DTO;
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
        public async Task<EventCategoryDTO> CreateAsync(EventCategoryDTO createDto)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            if (createDto.ImageFile != null)
            {
                createDto.ThumbnailUrl = await _fileService.UploadFileAsync(createDto.ImageFile, "images/client");
            }
            createDto.Slug = _slugService.GenerateSlug(createDto.Name!);
            createDto.CreatedDate = DateTime.Now;
            createDto.ModifiedDate = createDto.CreatedDate;

            // Sử dụng Mapster để chuyển đổi từ DTO sang entity
            var categoryEvent = createDto.Adapt<CategoryEvents>();
            categoryEvent.Status = true; // Mặc định active

            // Thêm vào repository
            await _categoryEventRepository.AddAsync(categoryEvent);
            
            // Lưu thay đổi
            await _unitOfWork.CompleteAsync();

            // Sử dụng Mapster để chuyển đổi từ entity sang DTO
            return categoryEvent.Adapt<EventCategoryDTO>();
        }

        public async Task DeleteAsync(int id)
        {
            // Kiểm tra category có tồn tại không
            var categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
            if (categoryEvent == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với id: {id}");
            }

            // Thực hiện soft delete
            await _categoryEventRepository.SoftDeleteAsync(categoryEvent);
            
            // Lưu thay đổi
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
            return categoryEvent != null && !categoryEvent.IsDeleted;
        }

        public async Task<IEnumerable<EventCategoryDTO>> GetAllAsync()
        {
            // Lấy tất cả category chưa bị xóa mềm
            var categories = await _categoryEventRepository.GetAllAsync();
            categories = categories.Where(c => !c.IsDeleted && c.Status).ToList();

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

            if (updateDto.ImageFile != null)
            {
                updateDto.ThumbnailUrl = await _fileService.UploadFileAsync(updateDto.ImageFile, "images/client");
            }

            updateDto.CreatedDate = categoryEvent.CreatedDate;
            updateDto.Slug = _slugService.GenerateSlug(updateDto.Name!);
            updateDto.ModifiedDate = DateTime.Now;

            updateDto.Adapt(categoryEvent);

            await _categoryEventRepository.UpdateAsync(categoryEvent);
            await _unitOfWork.CompleteAsync();

            return categoryEvent.Adapt<EventCategoryDTO>();
        }
    }
}

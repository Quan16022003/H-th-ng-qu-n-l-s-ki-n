using Constracts.EventCategory;
using Domain.Repositories;
using Services.Abtractions;
using Mapster;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Services
{
    using EventCategoryResult = Result<EventCategoryDTO>;
    using EventCategoryListResult = Result<IEnumerable<EventCategoryDTO>>;

    internal sealed class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryEventRepository _categoryEventRepository;
        private readonly IFileService _fileService;
        private readonly ISlugService _slugService;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IFileService fileService,
            ISlugService slugService,
            ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryEventRepository = _unitOfWork.CategoryEventRepository;
            _fileService = fileService;
            _slugService = slugService;
            _logger = logger;
        }

        public async Task<Result<int>> CreateAsync(EventCategoryCreationDto createDto)
        {
            if (IsCategoryNameDuplicate(createDto.Name))
            {
                return Result<int>.Failure(CategoryError.DuplicateName);
            }

            var thumbnailUrl = await _fileService.UploadFileAsync(createDto.ImageFile, "images/client");
            var categoryEvent = new CategoryEvents
            {
                Name = createDto.Name,
                Description = createDto.Description,
                ThumbnailUrl = thumbnailUrl,
                Slug = GenerateSlug(createDto.Name),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = true
            };

            try
            {
                await _categoryEventRepository.AddAsync(categoryEvent);
                await _unitOfWork.CompleteAsync();
                return Result<int>.Success(categoryEvent.Id);
            }
            catch
            {
                return Result<int>.Failure(CategoryError.CreationFailed);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (!await IsExist(id))
            {
                return Result.Failure(CategoryError.NotFound);
            }
            if (IsCategoryInUse(id))
            {
                return Result.Failure(CategoryError.InUse);
            }

            try
            {
                // Implement the deletion logic here
                var category = await _unitOfWork.CategoryEventRepository.GetByIdAsync(id);
                category!.IsDeleted = true;

                await _unitOfWork.CategoryEventRepository.UpdateAsync(category);
                await _unitOfWork.CompleteAsync();

                return Result.Success();
            }
            catch
            {
                return Result.Failure(CategoryError.DeleteFailed);
            }
        }

        public async Task<EventCategoryListResult> GetAllAsync(string type = "", string query = "")
        {
            try
            {
                _logger.LogInformation($"Type: {type}, Query: {query}");
                IEnumerable<CategoryEvents> categories = string.IsNullOrEmpty(query) ? 
                    await _categoryEventRepository.GetManyAsync(
                        filter: e => !e.IsDeleted) : 
                    await _categoryEventRepository.GetManyAsync(
                        filter: e => type == "Equal" 
                            ? e.Name.ToLower().Equals(query.ToLower()) && !e.IsDeleted
                            : e.Name.ToLower().Contains(query.ToLower()) && !e.IsDeleted);

                var categoryDtos = categories.Adapt<IEnumerable<EventCategoryDTO>>();
                return EventCategoryListResult.Success(categoryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return EventCategoryListResult.Failure(CategoryError.GetAllFailed);
            }
        }

        public async Task<EventCategoryResult> GetByIdAsync(int id)
        {
            try
            {
                var categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
                if (categoryEvent == null)
                {
                    return EventCategoryResult.Failure(CategoryError.NotFound);
                }
                var categoryDto = categoryEvent.Adapt<EventCategoryDTO>();
                return EventCategoryResult.Success(categoryDto);
            }
            catch
            {
                return EventCategoryResult.Failure(CategoryError.GetFailed);
            }
        }

        public async Task<Result<EventCategoryDTO>> GetBySlugAsync(string slug)
        {
            try
            {
                var categoryEvent = await _categoryEventRepository.GetBySlugAsync(slug);
                if (categoryEvent == null)
                {
                    return Result<EventCategoryDTO>.Failure(CategoryError.NotFound);
                }
                var categoryDto = categoryEvent.Adapt<EventCategoryDTO>();
                return Result<EventCategoryDTO>.Success(categoryDto);
            }
            catch
            {
                return Result<EventCategoryDTO>.Failure(CategoryError.GetFailed);
            }
        }

        public async Task<Result<int>> UpdateAsync(EventCategoryUpdateDto updateDto)
        {
            ArgumentNullException.ThrowIfNull(updateDto);
            ArgumentNullException.ThrowIfNull(updateDto.Id);

            _logger.LogInformation("Updating category with Id: {Id}", updateDto.Id);

            try
            {
                var categoryEvent = await _categoryEventRepository.GetByIdAsync(updateDto.Id);
                
                if (categoryEvent == null)
                {
                    _logger.LogWarning("Category not found with Id: {Id}", updateDto.Id);
                    return Result<int>.Failure(CategoryError.NotFound);
                }

                if (categoryEvent.Name != updateDto.Name && IsCategoryNameDuplicate(updateDto.Name))
                {
                    _logger.LogWarning("Duplicate category name detected during update: {Name}", updateDto.Name);
                    return Result<int>.Failure(CategoryError.DuplicateName);
                }

                categoryEvent.Name = updateDto.Name;
                categoryEvent.Description = updateDto.Description;
                categoryEvent.Slug = GenerateSlug(updateDto.Name);

                if (updateDto.ImageFile != null)
                {
                    categoryEvent.ThumbnailUrl = await _fileService.UploadFileAsync(updateDto.ImageFile, "images/client");
                }

                await _categoryEventRepository.UpdateAsync(categoryEvent);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Category with Id: {Id} updated successfully", categoryEvent.Id);
                return Result<int>.Success(categoryEvent.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update category with Id: {Id}", updateDto.Id);
                return Result<int>.Failure(CategoryError.UpdateFailed);
            }
        }

        private async Task<bool> IsExist(int id)
        {
            var categoryEvent = await _categoryEventRepository.GetByIdAsync(id);
            return categoryEvent != null && !categoryEvent.IsDeleted;
        }

        private bool IsCategoryNameDuplicate(string name) => _categoryEventRepository.IsNameDuplicate(name);

        private bool IsCategoryInUse(int id) => _categoryEventRepository.IsCategoryInUse(id);

        private string GenerateSlug(string name)
        {
            var slug = _slugService.GenerateSlug(name);
            int index = 2;
            while (_categoryEventRepository.HasSlug(slug))
            {
                slug = $"{_slugService.GenerateSlug(name)}-{index}";
                index++;
            }
            return slug;
        }
    }
}
using Constracts.DTO;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Constracts.EventCategory
{
    public class EventCategoryDTO : BaseDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [Display(Name = "Tên")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả.")]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Required]
        public string? Slug { get; set; }

        public string? ThumbnailUrl { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn một ảnh.")]
        public IFormFile? ImageFile { get; set; }

        public bool Status { get; set; }
    }
}


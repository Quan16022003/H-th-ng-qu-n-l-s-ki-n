using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Constracts.DTO
{
    public class EventCategoryDTO : BaseDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Slug { get; set; }

        public string? ThumbnailUrl { get; set; }

        public IFormFile? ImageFile { get; set; }

        public bool Status { get; set; }
    }
}


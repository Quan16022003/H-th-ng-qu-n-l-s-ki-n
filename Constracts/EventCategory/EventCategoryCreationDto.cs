using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Constracts.EventCategory
{
    public class EventCategoryCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
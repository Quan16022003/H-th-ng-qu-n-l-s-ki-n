using System.ComponentModel.DataAnnotations;

namespace Constracts.EventCategory
{
    public class CreateEventCategoryDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
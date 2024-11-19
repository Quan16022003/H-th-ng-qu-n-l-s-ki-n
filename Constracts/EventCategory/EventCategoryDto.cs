using System.ComponentModel.DataAnnotations;

namespace Constracts.EventCategory
{
    public class EventCategoryDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
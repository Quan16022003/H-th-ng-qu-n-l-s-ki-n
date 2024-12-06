using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Constracts.EventCategory
{
    public class EventCategoryUpdateDto
    {
        [Required]
        public required int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [Display(Name = "Tên")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả.")]
        [Display(Name = "Mô tả")]
        public required string Description { get; set; }
        
        public IFormFile? ImageFile { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Description: {Description}";
        }
    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class EventDetailDTO : BaseDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
        public string? OrganizerId { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublic { get; set; } = false;
        public IFormFile? ImageFile { get; set; }
    }
}

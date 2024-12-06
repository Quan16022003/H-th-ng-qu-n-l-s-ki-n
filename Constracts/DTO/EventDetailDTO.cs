using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class EventDetailDTO : BaseDTO
    {
        public string Slug { get; set; }
        public string OrganizerId { get; set; }
        public int CategoryId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        public IFormFile? ThumbnailUrl { get; set; }
    }
}

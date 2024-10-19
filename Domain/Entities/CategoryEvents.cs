using Domain.Commons;

namespace Domain.Entities
{
    public class CategoryEvents : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool Status { get; set; } = true;

        public IEnumerable<Events> Events { get; set; }
    }
}
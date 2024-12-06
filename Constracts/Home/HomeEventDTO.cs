using Domain.Entities;

namespace Constracts.Home
{
    public class HomeEventDTO
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VenueName { get; set; }
        public int MinTicketPrice { get; set; }
        public DateTime? StartDate { get; set; }

        public HomeEventDTO(Events e)
        {
            Slug = e.Slug;
            Title = e.Title;
            Description = e.Description;
            CategoryName = e.CategoryEvent.Name;
            ThumbnailUrl = e.ThumbnailUrl;
            VenueName = e.VenueName;
            MinTicketPrice = e.Tickets?.Any() is true ? e.Tickets.Min(t => t.Price) : 0;
            StartDate = e.StartDate;
        }
    }
}
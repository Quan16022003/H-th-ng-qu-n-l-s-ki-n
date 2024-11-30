using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.Home
{
    public class HomeEventDTO
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public String CategoryName { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VenueName { get; set; }
        public int MinTicketPrice { get; set; }
        public DateTime? StartDate { get; set; }

        public HomeEventDTO(Events e)
        {
            Slug = e.Slug;
            Title = e.Title;
            Description = e.Description;
            ThumbnailUrl = e.ThumbnailUrl;
            VenueName = e.VenueName;
            StartDate = e.StartDate;
            CategoryName = e.CategoryEvent.Name;
            MinTicketPrice = e.Tickets?.Any() == true
                ? e.Tickets.Min(t => t.Price)
                : 0;
        }
    }
}

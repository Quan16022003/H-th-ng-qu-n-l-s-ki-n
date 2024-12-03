using Constracts.DTO;
using Constracts.Home;
using Constracts.EventCategory;

namespace Web.ViewModels;

public class HomeViewModel
{
    public IEnumerable<VenueDTO> Venues { get; set; }
    public IEnumerable<EventCategoryDTO> EventCategoríes { get; set; }
    public IEnumerable<HomeEventDTO> FeaturedEvents { get; set; }
    public IEnumerable<HomeEventDTO> UpcomingEvents { get; set; }
    public IEnumerable<HomeEventDTO> BestSellerEvents { get; set; }
    public HomeViewModel()
    {
        Venues = new List<VenueDTO>();
        EventCategoríes = new List<EventCategoryDTO>();
        FeaturedEvents = new List<HomeEventDTO>();
        UpcomingEvents = new List<HomeEventDTO>();
        BestSellerEvents = new List<HomeEventDTO>();
    }
}

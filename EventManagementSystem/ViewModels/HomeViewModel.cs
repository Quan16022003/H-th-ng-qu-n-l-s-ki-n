using Constracts.Home;
using Constracts.EventCategory;

namespace Web.ViewModels;

public class HomeViewModel
{
    public IEnumerable<EventCategoryDTO> EventCategoríes { get; set; }
    public IEnumerable<HomeEventDTO> FeaturedEvents { get; set; }
    public IEnumerable<HomeEventDTO> UpcomingEvents { get; set; }
    public IEnumerable<HomeEventDTO> BestSellerEvents { get; set; }
    public HomeViewModel()
    {
        EventCategoríes = new List<EventCategoryDTO>();
        FeaturedEvents = new List<HomeEventDTO>();
        UpcomingEvents = new List<HomeEventDTO>();
        BestSellerEvents = new List<HomeEventDTO>();
    }
}

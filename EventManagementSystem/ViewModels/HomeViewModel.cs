using Constracts.Home;

namespace Web.ViewModels;

public class HomeViewModel
{
    public IEnumerable<HomeEventDTO> FeaturedEvents { get; set; }
    public IEnumerable<HomeEventDTO> UpcomingEvents { get; set; }
}

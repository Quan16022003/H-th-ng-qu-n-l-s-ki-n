using Constracts.Home;

namespace Web.ViewModels;

public class HomeViewModel
{
    public IEnumerable<HomeEventDto> FeaturedEvents { get; set; }
    public IEnumerable<HomeEventDto> UpcomingEvents { get; set; }
}

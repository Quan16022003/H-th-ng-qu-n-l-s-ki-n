namespace Web.ViewModels;

public class HomeViewModel
{
    public IEnumerable<EventCardViewModel> FeaturedEvents { get; set; }
    public IEnumerable<EventCardViewModel> UpcomingEvents { get; set; }
}

namespace Web.ViewModels;
public class EventCardViewModel
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string ThumbnailUrl { get; set; }
    public string VenueName { get; set; }
    public bool IsOnline { get; set; }
    public bool IsFree { get; set; }
    public bool IsOnetime { get; set; }
    public DateTime? StartDate { get; set; }
} 
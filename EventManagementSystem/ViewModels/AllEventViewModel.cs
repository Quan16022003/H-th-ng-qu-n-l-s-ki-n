   using Constracts.Home;

namespace Web.ViewModels
{
    public class AllEventViewModel
    {
        public string? query { get; set; }

        public IEnumerable<HomeEventDTO> AllEvents { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < TotalPages);
    }
}

using Constracts.DTO;

namespace Services.Abtractions
{
    public interface IVenueService
    {
        Task<IEnumerable<VenueDTO>> GetCitiesSortedByEventCountAsync();   
    }
}
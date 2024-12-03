using Constracts.DTO;
using Domain.Repositories;
using Services.Abtractions;

namespace Services
{
    public class VenueService : IVenueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventRepository _eventRepository;
        public VenueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventRepository = _unitOfWork.EventRepository;
        }
        public async Task<IEnumerable<VenueDTO>> GetCitiesSortedByEventCountAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            var venues = events
                .Where(e => !string.IsNullOrEmpty(e.City))
                .GroupBy(e => e.City!)
                .Select(e => new VenueDTO()
                {
                    VenueName = e.First().VenueName,
                    City = e.Key,
                    District = e.First().District,
                    Ward = e.First().Ward,
                    Address = e.First().Address
                });
            return venues;
        }
    }
}
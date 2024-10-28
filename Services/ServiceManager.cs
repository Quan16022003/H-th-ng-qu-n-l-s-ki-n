
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abtractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        //private readonly Lazy<IOwnerService> _lazyOwnerService;
        private readonly Lazy<IEventService> _lazyEventService;

        public ServiceManager(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEventRepository eventRepository)
        {
            _lazyEventService = new Lazy<IEventService>(() => new EventService(eventRepository, unitOfWork));
            //_lazyOwnerService = new Lazy<IOwnerService>(() => new OwnerService(repositoryManager));
        }

        //public IOwnerService OwnerService => _lazyOwnerService.Value;
        public IEventService EventService => _lazyEventService.Value;
    }
}

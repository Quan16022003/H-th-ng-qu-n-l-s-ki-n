
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
        private readonly Lazy<ICategoryEventService> _lazyCategoryEventService;

        public ServiceManager(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEventRepository eventRepository,
            ICategoryEventRepository categoryEventRepository)
        {
            _lazyEventService = new Lazy<IEventService>(() => new EventService(eventRepository, unitOfWork));
            _lazyCategoryEventService = new Lazy<ICategoryEventService>(() => new CategoryEventService(categoryEventRepository, unitOfWork));
            //_lazyOwnerService = new Lazy<IOwnerService>(() => new OwnerService(repositoryManager));
        }

        //public IOwnerService OwnerService => _lazyOwnerService.Value;
        public IEventService EventService => _lazyEventService.Value;
        public ICategoryEventService CategoryEventService => _lazyCategoryEventService.Value;
    }
}

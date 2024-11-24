
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Abtractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        //private readonly Lazy<IOwnerService> _lazyOwnerService;
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IEventService> _lazyEventService;
        private readonly Lazy<IEventCategoryService> _lazyEventCategoryService;

        public ServiceManager(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IFileService fileService)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(unitOfWork));
            _lazyEventService = new Lazy<IEventService>(() => new EventService(unitOfWork, loggerFactory.CreateLogger<EventService>(), fileService));
            _lazyEventCategoryService = new Lazy<IEventCategoryService>(() => new EventCategoryService(unitOfWork, fileService));
            //_lazyOwnerService = new Lazy<IOwnerService>(() => new OwnerService(repositoryManager));
        }

        //public IOwnerService OwnerService => _lazyOwnerService.Value;
        public IUserService UserService => _lazyUserService.Value;
        public IEventService EventService => _lazyEventService.Value;
        public IEventCategoryService EventCategoryService => _lazyEventCategoryService.Value;
    }
}

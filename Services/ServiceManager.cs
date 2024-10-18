
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abtractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        //private readonly Lazy<IOwnerService> _lazyOwnerService;

        public ServiceManager(IUnitOfWork unitOfWork, 
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            //_lazyOwnerService = new Lazy<IOwnerService>(() => new OwnerService(repositoryManager));
        }

        //public IOwnerService OwnerService => _lazyOwnerService.Value;
    }
}

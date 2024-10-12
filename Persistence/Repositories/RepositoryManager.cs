
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IOwnerRepository> _ownerRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        public RepositoryManager(RepositoryDbContext dbContext)
        {
            _ownerRepository = new Lazy<IOwnerRepository>(() => new OwnerRepository(dbContext));
            _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(dbContext));
            _unitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IAccountRepository AccountRepository => _accountRepository.Value;

        public IOwnerRepository OwnerRepository => _ownerRepository.Value;

        public IUnitOfWork UnitOfWork => _unitOfWork.Value;
    }
}

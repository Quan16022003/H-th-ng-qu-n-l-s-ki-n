 

namespace Domain.Exceptions
{
    public sealed class AccountDoesNotBelongToOwnerException : BadRequestException
    {
        public AccountDoesNotBelongToOwnerException(Guid accountId, Guid ownerId)
        : base($"The account with the identifier {accountId} does not belong to the owner with the identifier {ownerId}.") 
        { 
        }
    }
}

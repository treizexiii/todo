using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories;

public interface IClaimsRepository
{
    Task<IEnumerable<Claim>> GetClaimsAsync();
    Task<IEnumerable<Claim>> GetClaimsAsync(Guid userId);
    Task<Claim?> GetClaimAsync(Guid claimId);
    Task AddClaimAsync(Claim claim);
    Task RemoveClaimAsync(Claim claim);
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistAsync(string name);
    Task RemoveUserClaimAsync(UserClaim userClaim);
}
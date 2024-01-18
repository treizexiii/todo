using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;

namespace Authentication.Persistence.RepositoriesImp;

public class ClaimsRepository : IClaimsRepository
{
    public Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Claim>> GetClaimsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Claim?> GetClaimAsync(Guid nodeId)
    {
        throw new NotImplementedException();
    }

    public Task AddClaimAsync(Claim claim)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClaimAsync(Claim claim)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserClaimAsync(UserClaim userClaim)
    {
        throw new NotImplementedException();
    }
}
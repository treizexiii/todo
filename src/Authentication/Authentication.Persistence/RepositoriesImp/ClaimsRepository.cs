using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.RepositoriesImp;

public class ClaimsRepository(IdentityDb context) : IClaimsRepository
{
    public async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        return await context.Claims.ToListAsync();
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(Guid userId)
    {
        return await context.Claims.Where(x => x.UserClaims.Any(y => y.UserId == userId)).ToListAsync();
    }

    public async Task<Claim?> GetClaimAsync(Guid claimId)
    {
        return await context.Claims.FirstOrDefaultAsync(c => c.Id == claimId);
    }

    public async Task AddClaimAsync(Claim claim)
    {
        await context.Claims.AddAsync(claim);
    }

    public Task RemoveClaimAsync(Claim claim)
    {
        context.Claims.Remove(claim);
        return Task.FromResult(Task.CompletedTask);
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        return await context.Claims.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistAsync(string name)
    {
        return await context.Claims.AnyAsync(x => x.Name == name);
    }

    public Task RemoveUserClaimAsync(UserClaim userClaim)
    {
        context.UserClaims.Remove(userClaim);
        return Task.FromResult(Task.CompletedTask);
    }
}
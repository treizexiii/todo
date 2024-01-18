using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserAsync(string username);
    Task<User?> GetUserByGuidAsync(Guid userId);
    Task AddUserAsync(User user);
    Task AddUserClaimAsync(UserClaim userClaim);
    Task RemoveUserClaimAsync(Guid userGuid, Guid claimGuid);
    Task CreateClaimAsync(Claim claim);
    Task RemoveUserAsync(User user);
    Task<IEnumerable<Role>> GetRolesAsync();
    Task UpdateUser(User user);
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistAsync(string userName);
    Task AddTokenAsync(Token data);
}

public interface IClaimsRepository
{
    Task<IEnumerable<Claim>> GetClaimsAsync();
    Task<IEnumerable<Claim>> GetClaimsAsync(Guid userId);
    Task<Claim?> GetClaimAsync(Guid nodeId);
    Task AddClaimAsync(Claim claim);
    Task RemoveClaimAsync(Claim claim);
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistAsync(string name);
    Task RemoveUserClaimAsync(UserClaim userClaim);
}
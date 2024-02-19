using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;

namespace Authentication.Domain.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserAsync(string username);
    Task<User?> GetUserByGuidAsync(Guid userId);
    Task AddUserAsync(User user);
    Task<Role?> GetRoleAsync(RoleEnum name);
    Task UpdateUser(User user);
    Task<bool> IsExistAsync(string userName);
    Task AddTokenAsync(Token data);
}

public interface IClaimsRepository
{
    Task<IEnumerable<Claim>> GetClaimsAsync();
    Task<IEnumerable<Claim>> GetClaimsAsync(Guid userId);
    Task<Claim?> GetClaimAsync(Guid claimId);
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistAsync(string name);
    Task AddClaimAsync(Claim claim);
    Task AddUserClaimAsync(UserClaim userClaim);
    Task RemoveClaimAsync(Claim claim);
    Task RemoveUserClaimAsync(UserClaim userClaim);
}
using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserAsync(string username);
    Task<User?> GetUserByGuidAsync(Guid userId);
    Task AddUserAsync(User user);
    Task AddUserClaimAsync(UserClaim userClaim);
    Task RemoveUserAsync(User user);
    Task<IEnumerable<Role>> GetRolesAsync();
    Task UpdateUser(User user);
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistAsync(string userName);
    Task AddTokenAsync(Token data);
}
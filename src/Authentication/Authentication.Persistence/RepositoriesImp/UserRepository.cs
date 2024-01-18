using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;
using Authentication.Domain.Repositories;
using Authentication.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.RepositoriesImp;

public class UserRepository(IdentityDb context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public Task<User?> GetUserAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByGuidAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public Task AddUserClaimAsync(UserClaim userClaim)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserClaimAsync(Guid userGuid, Guid claimGuid)
    {
        throw new NotImplementedException();
    }

    public Task CreateClaimAsync(Claim claim)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        var roles = new List<Role>
        {
            new()
            {
                Name = RoleEnum.User,
                Id = Guid.NewGuid()
            },
            new()
            {
                Name = RoleEnum.Admin,
                Id = Guid.NewGuid()
            }
        };
        return roles;
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsExistAsync(string userName)
    {
        return await context.Users.AnyAsync(x => x.Username == userName);
    }

    public Task AddTokenAsync(Token data)
    {
        throw new NotImplementedException();
    }
}
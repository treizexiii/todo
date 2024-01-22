using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Persistence.Database;
using Authentication.Persistence.PersistentEntities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.RepositoriesImp;

public class UserRepository(IdentityDb context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User?> GetUserByGuidAsync(Guid userId)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task AddUserAsync(User user)
    {
        var dbUser = new DbUser
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
            Salt = user.Salt,
            IsActivated = user.IsActivated,
            ActivatedAt = user.ActivatedAt,
            IsDeleted = user.IsDeleted,
            DeletedAt = user.DeletedAt,
        };
        await context.Users.AddAsync(dbUser);
    }

    public async Task AddUserClaimAsync(UserClaim userClaim)
    {
        await context.UserClaims.AddAsync(userClaim);
    }

    public Task RemoveUserAsync(User user)
    {
        context.Users.Remove(user);
        return Task.FromResult(Task.CompletedTask);
    }

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        return await context.Roles.ToListAsync();
    }

    public Task UpdateUser(User user)
    {

        context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        return await context.Users.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistAsync(string userName)
    {
        return await context.Users.AnyAsync(x => x.Username == userName);
    }

    public async Task AddTokenAsync(Token data)
    {
        await context.Tokens.AddAsync(data);
    }
}
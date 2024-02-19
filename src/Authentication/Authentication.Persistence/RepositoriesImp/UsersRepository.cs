using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;
using Authentication.Domain.Repositories;
using Authentication.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.RepositoriesImp;

public class UsersRepository(IdentityDb context) : IUsersRepository
{
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserAsync(string username)
    {
        return await context.Users
            .Where(u => u.Username == username && !u.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByGuidAsync(Guid userId)
    {
        return await context.Users
            .Where(u => u.Id == userId && !u.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public Task RemoveUserClaimAsync(Guid userGuid, Guid claimGuid)
    {
        throw new NotImplementedException();
    }

    public Task CreateClaimAsync(Claim claim)
    {
        throw new NotImplementedException();
    }

    public async Task<Role?> GetRoleAsync(RoleEnum name)
    {
        return await context.Roles.FirstOrDefaultAsync(x => x.Name == name);
    }

    public Task UpdateUser(User user)
    {
        context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task<bool> IsExistAsync(string userName)
    {
        return await context.Users.AnyAsync(x => x.Username == userName && !x.IsDeleted);
    }

    public Task AddTokenAsync(Token data)
    {
        throw new NotImplementedException();
    }
}
using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;
using Authentication.Domain.Exceptions;
using Authentication.Domain.Repositories;
using Authentication.Domain.Services;
using Authentication.Tools;
using Authentication.Wrappers;

namespace Authentication.Services.Admin;

public interface IAdminUser
{
    Task<int> AddUser(NewUserDto registerDto);
    Task<int> RemoveUser(Guid id);
    Task<IEnumerable<UserDto>> GetUsers();
    Task<int> AddUserClaim(Guid userId, Guid claimId);
}

public interface IAdminClaim
{
    Task<IEnumerable<ClaimDto>> GetClaims();
    Task<int> AddClaim(NewClaimDto claimDto);
    Task<int> RemoveClaim(Guid id);
}

public class AdminService(
    IUsersRepository usersRepository,
    ISecretService secretService,
    IClaimsRepository claimsRepository)
    : IAdminUser, IAdminClaim
{
    public async Task<int> AddUser(NewUserDto registerDto)
    {
        if (!registerDto.IsValid())
        {
            return (int)ResultCode.BadRequest;
        }

        if (await usersRepository.IsExistAsync(registerDto.Email))
        {
            return (int)ResultCode.AlreadyExists;
        }

        var role = await usersRepository.GetRoleAsync(RoleEnum.User);
        if (role == null)
        {
            throw new FileNotFoundException("Role not found");
        }

        var hash = DataHasher.Hash(registerDto.Password);

        var user = new User
        {
            Id = registerDto.Id ?? Guid.NewGuid(),
            Username = registerDto.Email,
            Email = registerDto.Email,
            IsActivated = true,
            IsDeleted = false,
            RoleId = role.Id,
        };

        var secret = new Secret
        {
            Id = Guid.NewGuid(),
            OwnerId = user.Id,
            Name = SecretType.Password,
            Value = hash.hash,
            Salt = hash.salt,
        };

        await usersRepository.AddUserAsync(user);
        await secretService.CreateSecret(secret);

        return (int)ResultCode.Success;
    }

    public async Task<int> RemoveUser(Guid id)
    {
        var user = await usersRepository.GetUserByGuidAsync(id);
        if (user == null)
        {
            return (int)ResultCode.NotFound;
        }

        user.IsDeleted = true;
        user.DeletedAt = DateTimeOffset.Now;
        await usersRepository.UpdateUser(user);

        return (int)ResultCode.Success;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await usersRepository.GetUsersAsync();
        var dto = new List<UserDto>();
        foreach (var user in users)
        {
            var nodes = user.UserClaims.Select(x => x.Claim).Select(ClaimDto.GetClaimDto);
            dto.Add(UserDto.GetUserDto(user, nodes));
        }

        return dto;
    }

    public async Task<int> AddUserClaim(Guid userId, Guid claimId)
    {
        var user = await usersRepository.GetUserByGuidAsync(userId);
        var claim = await claimsRepository.GetClaimAsync(claimId);

        if (user == null || claim == null)
        {
            return (int)ResultCode.NotFound;
        }

        if (user.UserClaims.Any(x => x.ClaimId == claim.Id))
        {
            return (int)ResultCode.AlreadyExists;
        }

        await claimsRepository.AddUserClaimAsync(new UserClaim
        {
            UserId = user.Id,
            ClaimId = claim.Id,
        });

        return (int)ResultCode.Success;
    }

    public async Task<IEnumerable<ClaimDto>> GetClaims()
    {
        var claims = await claimsRepository.GetClaimsAsync();
        var dto = new List<ClaimDto>();
        foreach (var claim in claims)
        {
            dto.Add(ClaimDto.GetClaimDto(claim));
        }

        return dto;
    }

    public async Task<int> AddClaim(NewClaimDto claimDto)
    {
        var claim = new Claim
        {
            Id = claimDto.Guid == Guid.Empty ? Guid.NewGuid() : claimDto.Guid,
            Name = claimDto.Name,
            Value = claimDto.Value,
            Type = claimDto.Type,
        };

        if (await claimsRepository.IsExistAsync(claim.Id) ||
            await claimsRepository.IsExistAsync(claim.Name))
        {
            return (int)ResultCode.AlreadyExists;
        }

        await claimsRepository.AddClaimAsync(claim);

        return (int)ResultCode.Success;
    }

    public async Task<int> RemoveClaim(Guid id)
    {
        var claim = await claimsRepository.GetClaimAsync(id);
        if (claim == null)
        {
            return (int)ResultCode.NotFound;
        }

        foreach (var userClaim in claim.UserClaims)
        {
            await claimsRepository.RemoveUserClaimAsync(userClaim);
        }

        await claimsRepository.RemoveClaimAsync(claim);
        return (int)ResultCode.Success;
    }
}